using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GhostPanel.Db;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using GhostPanel.Core.Data;
using System;
using Newtonsoft.Json;

namespace GhostPanel.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IContainer ApplicationContainer { get; private set; }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDataContext>(ServiceLifetime.Transient);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddJsonOptions(options => {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            var builder = new ContainerBuilder();
            builder.Populate(services);

            var fullConfig = Configuration.Get<GhostPanelConfig>();
            IRepository repository = SetUpDatabase.SetUpRepository(fullConfig.DatabaseConnectionString);

            builder.RegisterInstance(repository).SingleInstance();

            builder.RegisterType<BackgroundManager>().AsSelf().SingleInstance();

            ApplicationContainer = builder.Build();

            return new AutofacServiceProvider(ApplicationContainer);


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
