using BaseLibrary.Class.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ServerLibrary.Data;
using ServerLibrary.Helpers;
using ServerLibrary.Repositories.Contracts;
using ServerLibrary.Repositories.Implementations;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<JwtSection>(builder.Configuration.GetSection("JwtSection"));
var jwtSection = builder.Configuration.GetSection(nameof(JwtSection)).Get<JwtSection>();

builder.Services.AddDbContext<ApplicationDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Defualt") 
        ?? throw new InvalidOperationException("Sorry, your connection is not found"));
});

builder.Services.AddScoped<IGenaricRepository<GeneralDepartment>, GeneralDepartmentRepo>();
builder.Services.AddScoped<IGenaricRepository<Department>, DepartmentRepo>();
builder.Services.AddScoped<IGenaricRepository<Branch>,BranchRepo>();
builder.Services.AddScoped<IGenaricRepository<Town>,TownRepo>();
builder.Services.AddScoped<IGenaricRepository<City>, CityRepo>();
builder.Services.AddScoped<IGenaricRepository<Country>, CountryRepo>();
builder.Services.AddScoped<IGenaricRepository<Employee>, EmployeeRepo>();
builder.Services.AddScoped<IGenaricRepository<Sanction>, SanctionRepo>();
builder.Services.AddScoped<IGenaricRepository<Doctor>, DoctorRepo>();
builder.Services.AddScoped<IGenaricRepository<Vaction>, VacationRepo>();
builder.Services.AddScoped<IGenaricRepository<OverTime>, OvertimeRepo>();
builder.Services.AddScoped<IGenaricRepository<OverTimeType>, OvertimeTypeRepositries>();
builder.Services.AddScoped<IGenaricRepository<SanctionType>, SanctionTypeRepositries>();
builder.Services.AddScoped<IGenaricRepository<VactionType>, VacationTypeRepo>();

builder.Services.AddScoped<IUserAccount,UserAccount>();

builder.Services.AddCors(options => 
{
    options.AddPolicy("AllowBlazorWasm", builder =>
    builder.WithOrigins("http://localhost:5109", "https://localhost:7006")
    .AllowAnyHeader().AllowAnyMethod().AllowCredentials());
});

builder.Services.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(jwt => 
{
    jwt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidIssuer = jwtSection!.Issuer,
        ValidAudience = jwtSection.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection.SecertKey!))
    };

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowBlazorWasm");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
