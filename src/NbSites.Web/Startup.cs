﻿using Autofac;
using Autofac.Extensions.DependencyInjection;
using Demo.Libs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace NbSites.Web
{
    public class Startup
    {
        #region another register way

        //public ILifetimeScope AutofacContainer { get; private set; }

        //public IServiceProvider ConfigureServices(IServiceCollection services)
        //{
        //    services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

        //    var builder = new ContainerBuilder();
        //    builder.Populate(services);

        //    // Register your own things directly with Autofac
        //    builder.RegisterModule(new MyApplicationModule());
        //    AutofacContainer = builder.Build();

        //    // this will be used as the service-provider for the application!
        //    return new AutofacServiceProvider(AutofacContainer);
        //}

        //public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        //{
        //    if (env.IsDevelopment())
        //    {
        //        app.UseDeveloperExceptionPage();
        //    }

        //    app.UseMvc(routes =>
        //    {
        //        routes.MapRoute(
        //            name: "route_root",
        //            template: "{controller=Home}/{action=Index}/{id?}");
        //    });

        //}

        #endregion

        public ILifetimeScope AutofacContainer { get; private set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Don't build the container; that gets done for you by the factory.
            builder.RegisterModule(new MyApplicationModule());
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // If, for some reason, you need a reference to the built container, you
            // can use the convenience extension method GetAutofacRoot.
            this.AutofacContainer = app.ApplicationServices.GetAutofacRoot();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "route_root",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }

    public class MyApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<HelloService>().As<IHelloService>();
            builder.RegisterType<EmptyService>().SingleInstance().AsSelf();
        }
    }
}