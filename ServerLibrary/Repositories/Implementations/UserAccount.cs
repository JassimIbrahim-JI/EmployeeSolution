using BaseLibrary.Class.DTOs;
using BaseLibrary.Class.Entities;
using BaseLibrary.Class.Response;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ServerLibrary.Data;
using ServerLibrary.Helpers;
using ServerLibrary.Repositories.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


namespace ServerLibrary.Repositories.Implementations
{
    public class UserAccount(ApplicationDbContext _context, IOptions<JwtSection> _config) : IUserAccount
    {
     
        public async Task<GeneralResponse> CreateAsync(Register register)
        {
            if (register == null) return new GeneralResponse(false, "Invalid Request");

            var checkUser = await FindByEmail(register.Email!);
            if (checkUser != null) 
            {
                return new GeneralResponse(false, "User registered already");
            }

            //Save User
            var user = await AddToDatabase(new ApplicationUser()
            {
                Name = register.FullName,
                Email = register.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(register.Password)
            });

            //check, create and assign role
            var checkAdminRole = await _context.SystemRoles
                .FirstOrDefaultAsync(sr => sr.Name!.Equals(Constants.Admin));
            if (checkAdminRole == null) 
            {
                var createAdminRole = await AddToDatabase(new SystemRole() { Name = Constants.Admin });
                await AddToDatabase(new UserRole() { RoleId = createAdminRole.Id, UsertId = user.Id });
                return new GeneralResponse(true, "Account created!");
            }

            var checkUserRole = await _context.SystemRoles
                .FirstOrDefaultAsync(sr => sr.Name!.Equals(Constants.User));
            SystemRole response = new();
            if (checkUserRole == null) 
            {
                response = await AddToDatabase(new SystemRole() { Name = Constants.User });
                await AddToDatabase(new UserRole() { RoleId = response.Id, UsertId = user.Id });

            }
            else
            {
                await AddToDatabase(new UserRole() { RoleId = checkUserRole.Id, UsertId = user.Id });
            }
            return new GeneralResponse(true, "Account Created!");

        }

        private async Task<ApplicationUser> FindByEmail(string email) =>
            await _context.User.FirstOrDefaultAsync(u => u.Email!.ToLower()!.Equals(email!.ToLower()));

        private async Task<TData> AddToDatabase<TData>(TData model) 
        {
            var result = _context.Add(model!);
            await _context.SaveChangesAsync();
            return (TData) result.Entity;
        }
        public async Task<LoginResponse> SignInAsync(Login login)
        {
            if (login == null) return new LoginResponse(false, "model is empty");

            var user = await FindByEmail(login.Email!);
            if(user is null) 
            {
                return new LoginResponse(false, "user not found");
            }

            bool verfiyPassword = BCrypt.Net.BCrypt.Verify(login.Password, user.Password);
            if (!verfiyPassword) 
            {
                return new LoginResponse(false, "Email/Password not valid");
            }

            var userRole = await FindUserRole(user.Id);
            if (userRole is null) 
            {
                return new LoginResponse(false, "user role not found!");
            }

            var roleName = await FindRoleName(userRole.RoleId);
            if (roleName is null)
                return new LoginResponse(false, "user role not found!");

            string jwtToken = GenerateToken(user,roleName!.Name!);
            string refreshToken = GenerateRefreshToken();

            //save the refresh token to the db 

            var findUser = await _context.RefreshTokens.FirstOrDefaultAsync(r => r.UserId == user.Id);
            if (findUser is not null) 
            {
                findUser!.Token = refreshToken;
                await _context.SaveChangesAsync();
            }
            else 
            {
                await AddToDatabase(new RefreshTokenInfo() { Token = refreshToken, UserId = user.Id }); 
            }

            return new LoginResponse(true,"Login Successfully", jwtToken, refreshToken);
        }

        public string GenerateToken(ApplicationUser user, string role) 
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Value.SecertKey!));
            var credentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);

            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.Name!),
                new Claim(ClaimTypes.Email,user.Email!),
                new Claim(ClaimTypes.Role,role!),

            };

            var token = new JwtSecurityToken
                (
                issuer:_config.Value.Issuer,
                audience:_config.Value.Audience,
                claims:userClaims,
                expires:DateTime.Now.AddDays(1),
                signingCredentials:credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static string GenerateRefreshToken() =>
            Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        private async Task<SystemRole> FindRoleName(int roleId) =>
            await _context.SystemRoles.FirstOrDefaultAsync(u => u.Id == roleId);

        private async Task<UserRole> FindUserRole(int userId) =>
          await _context.UserRoles.FirstOrDefaultAsync(u => u.UsertId == userId);

        public async Task<LoginResponse> RefreshTokenAsync(RefreshToken token)
        {
            if (token is null) return new LoginResponse(false, "model is empty");

            var findToken = await _context.RefreshTokens
                .FirstOrDefaultAsync(t => t.Token!.Equals(token.Token));
             
            if(findToken is null) 
            {
                return new LoginResponse(false, "Refresh Token is required");
            }

            var user = await _context.User.FirstOrDefaultAsync(u => u.Id == findToken.UserId);
            if (user is null) 
            {
                return new LoginResponse(false, "refresh token could not be generated because user not found");
            }

            var userRole = await FindUserRole(user.Id);
            var roleName = await FindRoleName(userRole.RoleId);
            string jwtToken = GenerateToken(user,roleName.Name!);
            string refreshTokens = GenerateRefreshToken();

            var updatedRefreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(r => r.UserId == user.Id);
            if (updatedRefreshToken is null) 
            {
                return new LoginResponse(false, "refresh token could not be generated because user has not signin ");
            }

            updatedRefreshToken.Token = refreshTokens;
            await _context.SaveChangesAsync();
            return new LoginResponse(true, "Token refreshed Successfully", jwtToken, refreshTokens);
        }

        public async Task<List<ManageUser>> GetUsers()
        {
            var allUser = await GetApplicationUsers();
            var allUserRoles = await UserRoles();
            var allRoles = await SystemRoles();

            if(allUser.Count == 0 || allRoles.Count == 0) { return null!; }

            var users = new List<ManageUser>();
            foreach (var user in allUser) 
            {
             var userRoles = allUserRoles.FirstOrDefault(r => r.UsertId == user.Id);
             var roleName = allRoles.FirstOrDefault(x => x.Id == userRoles!.RoleId);
                users.Add(new ManageUser() { UserId = user.Id, Name = user.Name!, Email = user.Email, Role = roleName!.Name! });
            }
            return users;
        }

        public async Task<GeneralResponse> UpdateUser(ManageUser user)
        {
            var getRoles = (await SystemRoles()).FirstOrDefault(r => r.Name!.Equals(user.Role));
            var userRole = await _context.UserRoles.FirstOrDefaultAsync(u=>u.UsertId == user.UserId);
            userRole!.RoleId = getRoles!.Id;
            await _context.SaveChangesAsync();
            return new GeneralResponse(true, "User role Updated successfully");
        }

        public async Task<List<SystemRole>> GetRoles()
        {
            return await SystemRoles();
        }

        public async Task<GeneralResponse> DeleteUser(int userId)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.Id == userId);
            _context.User.Remove(user!);
            await _context.SaveChangesAsync();
            return new GeneralResponse(true, "User Successfully Deleted");
        }

        public async Task<string> GetUserImage(int id) =>
            (await GetApplicationUsers()).FirstOrDefault(u => u.Id == id)!.Picture!;

        public async Task<bool> UpdateProfile(UserProfile userProfile)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.Id == int.Parse(userProfile.Id));
            user!.Email = userProfile.Email;
            user!.Name = userProfile.Name;
            user!.Picture = userProfile.ProfilePicture;
            await _context.SaveChangesAsync();
            return true;    
        }


        private async Task<List<SystemRole>> SystemRoles() 
            => await _context.SystemRoles.AsNoTracking().ToListAsync();

        private async Task<List<UserRole>> UserRoles()
         => await _context.UserRoles.AsNoTracking().ToListAsync();

        private async Task<List<ApplicationUser>> GetApplicationUsers()
         => await _context.User.AsNoTracking().ToListAsync();

    }
}
