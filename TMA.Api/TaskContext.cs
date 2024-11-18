using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TMA.Api.Model;

namespace TMA.Api
{
    public class TaskContext : DbContext
    {

        private IConfiguration _configuration { get; }

        public TaskContext(DbContextOptions<TaskContext> options, IConfiguration configuration)
        : base(options)
        {
            _configuration = configuration;


        }
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RoleAction> RoleActions { get; set; }
        public DbSet<Actions> Actions { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //For Using SQLite Database
                //  optionsBuilder.UseSqlite("Data Source=taskmanagementapp.db");

                //For Using SQL Server Database
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            }
        }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Primary Keys
            modelBuilder.Entity<Tasks>().HasKey(t => t.TaskId);
            modelBuilder.Entity<User>().HasKey(u => u.UserId);
            modelBuilder.Entity<Role>().HasKey(r => r.RoleId);
            modelBuilder.Entity<UserRole>().HasKey(ur => ur.UserRoleId);
            modelBuilder.Entity<RoleAction>().HasKey(ra => ra.RoleActionId);
            modelBuilder.Entity<Actions>().HasKey(a => a.ActionId);

            // Foreign Key Relationships
            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany()
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Cascading delete if a user is removed

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany()
                .HasForeignKey(ur => ur.RoleId)
                .OnDelete(DeleteBehavior.Cascade); // Cascading delete if a role is removed

            modelBuilder.Entity<RoleAction>()
                .HasOne(ra => ra.Role)
                .WithMany()
                .HasForeignKey(ra => ra.RoleId)
                .OnDelete(DeleteBehavior.Cascade); // Cascading delete if a role is removed

            modelBuilder.Entity<RoleAction>()
                .HasOne(ra => ra.Action)
                .WithMany()
                .HasForeignKey(ra => ra.ActionId)
                .OnDelete(DeleteBehavior.Cascade); // Cascading delete if an action is removed

            // Seed initial data for Roles and Actions
            modelBuilder.Entity<User>().HasData(
               new User
               {
                   UserId = 1,
                   Email = "admin@example.com",
                   FirstName = "Admin",
                   LastName = "User",
                   Password = "Admin@123",
                   Address = "123 Admin St",
                   City = "Admin City",
                   PhoneNumber = "123-456-7890",
                   UserStatusId = 1,
                   IsDeleted = false,
                   CreatedBy = "system",
                   CreatedDate = DateTime.UtcNow
               },
               new User
               {
                   UserId = 2,
                   Email = "manager@example.com",
                   FirstName = "Manager",
                   LastName = "User",
                   Password = "Manager@123",
                   Address = "456 Manager Ave",
                   City = "Manager City",
                   PhoneNumber = "987-654-3210",
                   UserStatusId = 1,
                   IsDeleted = false,
                   CreatedBy = "system",
                   CreatedDate = DateTime.UtcNow
               },
               new User
               {
                   UserId = 3,
                   Email = "user@example.com",
                   FirstName = "Regular",
                   LastName = "User",
                   Password = "User@123",
                   Address = "789 User Blvd",
                   City = "User City",
                   PhoneNumber = "555-555-5555",
                   UserStatusId = 1, // Active
                   IsDeleted = false,
                   CreatedBy = "system",
                   CreatedDate = DateTime.UtcNow
               }
           );

            modelBuilder.Entity<UserRole>().HasData(
               new UserRole
               {
                   UserRoleId = 1,
                   UserId = 1,
                   RoleId = 1,
                   CreatedBy = "system",
                   CreatedDate = DateTime.UtcNow
               },
               new UserRole
               {
                   UserRoleId = 2,
                   UserId = 2,
                   RoleId = 2,
                   CreatedBy = "system",
                   CreatedDate = DateTime.UtcNow
               },
               new UserRole
               {
                   UserRoleId = 3,
                   UserId = 3,
                   RoleId = 3,
                   CreatedBy = "system",
                   CreatedDate = DateTime.UtcNow
               }
            );
            // Seed initial data for Roles and Actions
            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    RoleId = 1,
                    Name = "Admin",
                    RoleDescription = "Administrator with full access",
                    IsDeleted = false,
                    CreatedBy = "System",
                    CreatedDate = DateTime.Now
                },
                new Role
                {
                    RoleId = 2,
                    Name = "Manager",
                    RoleDescription = "Manager with limited management rights",
                    IsDeleted = false,
                    CreatedBy = "System",
                    CreatedDate = DateTime.Now
                },
                new Role
                {
                    RoleId = 3,
                    Name = "User",
                    RoleDescription = "Standard user with read-only access",
                    IsDeleted = false,
                    CreatedBy = "System",
                    CreatedDate = DateTime.Now
                }
            );

            modelBuilder.Entity<Actions>().HasData(
                new Actions
                {
                    ActionId = 1,
                    ActionName = "Create",
                    CreatedBy = "System",
                    CreatedDate = DateTime.Now
                },
                new Actions
                {
                    ActionId = 2,
                    ActionName = "Edit",
                    CreatedBy = "System",
                    CreatedDate = DateTime.Now
                },
                new Actions
                {
                    ActionId = 3,
                    ActionName = "Delete",
                    CreatedBy = "System",
                    CreatedDate = DateTime.Now
                },
                new Actions
                {
                    ActionId = 4,
                    ActionName = "View",
                    CreatedBy = "System",
                    CreatedDate = DateTime.Now
                }
            );

            modelBuilder.Entity<RoleAction>().HasData(
            // Admin Role (Full access)
            new RoleAction
            {
                RoleActionId = 1,
                RoleId = 1,
                ActionId = 1,
                HasFullAccess = true,
                HasReadOnly = false,
                CreatedBy = "System",
                CreatedDate = DateTime.Now
            },
            new RoleAction
            {
                RoleActionId = 2,
                RoleId = 1,
                ActionId = 2,
                HasFullAccess = true,
                HasReadOnly = false,
                CreatedBy = "System",
                CreatedDate = DateTime.Now
            },
            new RoleAction
            {
                RoleActionId = 3,
                RoleId = 1,
                ActionId = 3,
                HasFullAccess = true,
                HasReadOnly = false,
                CreatedBy = "System",
                CreatedDate = DateTime.Now
            },
            new RoleAction
            {
                RoleActionId = 4
            ,
                RoleId = 1,
                ActionId = 4,
                HasFullAccess = true,
                HasReadOnly = false,
                CreatedBy = "System",
                CreatedDate = DateTime.Now
            },

            // Manager Role (Limited access)
            new RoleAction
            {
                RoleActionId = 5,
                RoleId = 2,
                ActionId = 1,
                HasFullAccess = false,
                HasReadOnly = false,
                CreatedBy = "System",
                CreatedDate = DateTime.Now
            },
            new RoleAction
            {
                RoleActionId = 6,
                RoleId = 2,
                ActionId = 2,
                HasFullAccess = false,
                HasReadOnly = false,
                CreatedBy = "System",
                CreatedDate = DateTime.Now
            },
            new RoleAction
            {
                RoleActionId = 7,
                RoleId = 2,
                ActionId = 4,
                HasFullAccess = false,
                HasReadOnly = true,
                CreatedBy = "System",
                CreatedDate = DateTime.Now
            },

            // User Role (View-only access)
            new RoleAction
            {
                RoleActionId = 8,
                RoleId = 3,
                ActionId = 4,
                HasFullAccess = false,
                HasReadOnly = true,
                CreatedBy = "System",
                CreatedDate = DateTime.Now
            }

            );
        }
    }
}
