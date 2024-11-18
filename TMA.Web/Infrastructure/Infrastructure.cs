using Microsoft.EntityFrameworkCore;
using TMA.Api;
using TMA.Api.Repository;

namespace TMA.Infrastructure
{
    public static class Infrastructure
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<ITokenRepository, TokenRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleManagementRepository, RoleManagementRepository>();
        }
        public static void SetDBContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TaskContext>(options =>

            //For Using SQLite Database
            // options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

            //For Using SQL Server Database
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), x => x.MigrationsHistoryTable("__EFMigrationsHistory")));
        }
    }
}
