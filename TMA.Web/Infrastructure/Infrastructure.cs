using TMA.DAL;

namespace TMA.Infrastructure
{
    public static class Infrastructure
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<ITaskRepository, TaskRepository>();
        }
    }
}
