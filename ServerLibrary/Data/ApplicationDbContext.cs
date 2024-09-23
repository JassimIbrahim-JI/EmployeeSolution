using BaseLibrary.Class.Entities;
using Microsoft.EntityFrameworkCore;


namespace ServerLibrary.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>opt):base(opt)
        {
            
        }
      
        //General Department | Department | Branches
        public DbSet<GeneralDepartment> GeneralDepartments { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Branch> Branches { get; set; }

        //Users Authentication | User Role | System Roles
        public DbSet<ApplicationUser> User { get; set; }
        public DbSet<SystemRole> SystemRoles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RefreshTokenInfo> RefreshTokens { get; set; }


        // Country | Citys | Towns
        public DbSet<Country> Country { get; set; }
        public DbSet<City> Citys { get; set; }
        public DbSet<Town> Towns { get; set; }

        //Other | Vaction | Sanction | Doctor | Overtime 

        public DbSet<Vaction> Vactions { get; set; }
        public DbSet<VactionType> VactionsType { get; set; }

        public DbSet<Sanction> Sanctions { get; set; }
        public DbSet<SanctionType> SanctionTypes { get; set; }

        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<OverTime> OverTime { get; set; }
        public DbSet<OverTimeType> OverTimeTypes { get; set; }


        public DbSet<Employee> Employees { get; set; }


    }
}
