using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using TheTodoManager.BLL.Interfaces;
using TheTodoManager.BLL.Services;
using TheTodoManager.Models;

namespace TheTodoManager.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            //WIP: Trying to populate Options 
            services.AddOptions();
            services.Configure<BaseConfigOptions>(this.Configuration);

            // NOTE_TO_SELF: DAL is not in use as of now.
            #region DAL 
            /*            services.AddScoped(typeof(ITodoRepository), typeof(TodoRepository));
            */
            #endregion 

            #region BLL
            services.AddScoped(typeof(ITodoService), typeof(TodoService));

            #endregion

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TheTodoManager.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder().SetBasePath(env.ContentRootPath)
                                                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                                                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                                                    .AddEnvironmentVariables()
                                                    .Build();
            Configuration = builder;

            if (env.IsDevelopment() || env.IsEnvironment("DEV"))
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TheTodoManager.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
