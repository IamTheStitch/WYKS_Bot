using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WYKSBot.Core.Service.Items;
using WYKSBot.Core.Services.Profiles;
using WYKSBot.DAL;

namespace WYKSBot.Bots
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<RPGContext>(options =>
            {
                options.UseSqlServer("Server=(localdb)\\v11.0;Database=RPGContext;Trusted_Connection=True;MultipleActiveResultSets=true",
                    x => x.MigrationsAssembly("WYKSBot.DAL.Migrations"));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<IExperienceService, ExperienceService>();
            services.AddScoped<IProfileService, ProfileService>();

            var serviceProider = services.BuildServiceProvider();

            var bot = new Bot(serviceProider);
            services.AddSingleton(bot);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

        }
    }
}
