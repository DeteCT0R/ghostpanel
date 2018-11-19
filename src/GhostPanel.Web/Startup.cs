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
using System.Reflection;
using GhostPanel.BackgroundServices;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using GhostPanel.Web.Background;
using GhostPanel.Core.GameServerUtils;
using GhostPanel.Core.Providers;
using GhostPanel.Core.Config;
using GhostPanel.Core.Management.GameFiles;
using GhostPanel.Core.Managment.GameFiles;
using GhostPanel.Core.Background;
using GhostPanel.Core.Management;
using GhostPanel.Core.Management.Server;
using GhostPanel.Web.Modules;
using GhostPanel.Communication.Query;
using MediatR;
using GhostPanel.Core.Handlers.Commands;
using GhostPanel.Communication.Mediator.Handlers.Commands;
using GhostPanel.Core.Automation.StartProcess;
using Hangfire;

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
            services.AddCors();

            services.AddMediatR(typeof(CreateServerCommandHandler).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(QueryServerCommandHandler).GetTypeInfo().Assembly);

            GhostPanelConfig fullConfig = Configuration.Get<GhostPanelConfig>();


            services.AddHangfire(x => x.UseSqlServerStorage(fullConfig.HangFireDatabaseConnectionString));

            var builder = new ContainerBuilder();
            builder.Populate(services);

            builder.RegisterGeneric(typeof(Logger<>)).As(typeof(ILogger<>));

            

            builder.RegisterInstance(fullConfig).AsSelf().SingleInstance();
            IRepository repository = SetUpDatabase.SetUpRepository(fullConfig.DatabaseConnectionString);

            builder.RegisterType<SteamCredentialProvider>().As<ISteamCredentialProvider>().SingleInstance();
            builder.RegisterType<DefaultDirectoryProvider>().As<IDefaultDirectoryProvider>().SingleInstance();
            builder.RegisterType<PortAndIpProvider>().As<IPortAndIpProvider>().SingleInstance();


            // Game File Managers
            builder.RegisterType<LocalGameFileManager>().As<IGameFileManager>();
            builder.RegisterType<SteamCmdGameFiles>().As<IGameFileManager>();
            builder.RegisterType<GameFileManagerProvider>().As<IGameFileManagerProvider>().SingleInstance();

            builder.RegisterInstance(repository).SingleInstance();

            builder.RegisterType<PanelBackgroundTaskService>().As<IBackgroundService>().SingleInstance();


            builder.RegisterType<BackgroundManager>().AsSelf().SingleInstance();
            builder.RegisterType<ServerProcessManagerWin>().As<IServerProcessManager>().SingleInstance();
            builder.RegisterType<ServerProcessManagerProvider>().As<IServerProcessManagerProvider>().SingleInstance();

            builder.RegisterType<GameServerProvider>().As<IGameServerProvider>().SingleInstance();
            builder.RegisterType<GameQueryFactory>().As<IGameQueryFactory>().SingleInstance();
            builder.RegisterType<ServerStatService>().As<IServerStatService>().SingleInstance();
            builder.RegisterType<ServerStatsRefresh>().As<IHostedService>();
            builder.RegisterType<ScheduledTaskService>().As<IScheduledTaskService>().SingleInstance();
            builder.RegisterType<ScheduledTaskWorker>().As<IHostedService>();


            builder.RegisterType<PanelBackgroundWorker>().As<IHostedService>();


            builder.RegisterType<CommandlineProcessor>().As<ICommandlineProcessor>().SingleInstance();
            builder.RegisterType<GameServerManagerRefac>().As<IGameServerManager>().SingleInstance();

            // Before started actions
            builder.RegisterType<ProcessConfigFilesBeforeStarted>().As<IBeforeStartedAction>();

            builder.RegisterModule(new MediatorModule());

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
            app.UseCors(
                options => options.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader()
            );

            app.UseMvc();
        }
    }
}
