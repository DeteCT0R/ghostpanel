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
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using GhostPanel.Web.Background;
using GhostPanel.Core.Managment;
using GhostPanel.Core;
using GhostPanel.Core.GameServerUtils;
using GhostPanel.Core.Management;
using GhostPanel.Core.Providers;
using GhostPanel.Core.Config;
using GhostPanel.Core.Management.GameFiles;

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

            builder.RegisterGeneric(typeof(Logger<>)).As(typeof(ILogger<>));

            GhostPanelConfig fullConfig = Configuration.Get<GhostPanelConfig>();
            builder.RegisterInstance(fullConfig).AsSelf().SingleInstance();
            IRepository repository = SetUpDatabase.SetUpRepository(fullConfig.DatabaseConnectionString);

            builder.RegisterType<SteamCredentialProvider>().As<ISteamCredentialProvider>().SingleInstance();
            builder.RegisterType<SteamCredentialWrapper>().As<SteamCredentialWrapper>().SingleInstance();
            builder.RegisterType<GameServerManager>().As<IGameServerManager>();
            builder.RegisterType<GameServerManagerFactory>().As<GameServerManagerFactory>().SingleInstance();

            builder.RegisterInstance(repository).SingleInstance();
            builder.RegisterType<ServerManagerContainer>().SingleInstance();
            builder.RegisterType<PanelBackgroundTaskService>().As<IBackgroundService>().SingleInstance();
            builder.RegisterType<ServerStatusUpdateService>().As<IBackgroundService>().SingleInstance();

            builder.RegisterType<BackgroundManager>().AsSelf().SingleInstance();
            builder.RegisterType<ServerStatusBackgroundManager>().AsSelf().SingleInstance();
            builder.RegisterType<PanelBackgroundWorker>().As<IHostedService>();
            builder.RegisterType<ServerStatusBackgroundWorker>().As<IHostedService>();

            //ServerManagerContainer serverManagerContainer = new ServerManagerContainer(repository);
            //builder.RegisterInstance(serverManagerContainer).As<ServerManagerContainer>();
            

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
