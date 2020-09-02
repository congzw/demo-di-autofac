using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Demo.Libs;
using Microsoft.Extensions.DependencyInjection;

namespace DemoApp
{
    public class ChildScopeDemo
    {
        private const string RootLifetimeTag = "MyIsolatedRoot";

        public static void Run()
        {
            var serviceCollection = new ServiceCollection();
            //serviceCollection.AddLogging();

            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterType<HelloService>().As<IHelloService>();
            containerBuilder.RegisterType<EmptyService>().SingleInstance();

            var container = containerBuilder.Build();
            var rootProvider = new AutofacServiceProvider(container);

            using (var scope = container.BeginLifetimeScope(RootLifetimeTag, b =>
            {
                b.Populate(serviceCollection, RootLifetimeTag);
            }))
            {
                // This service provider will have access to global singletons
                // and registrations but the "singletons" for things registered
                // in the service collection will be "rooted" under this
                // child scope, unavailable to the rest of the application.
                //
                // Obviously it's not super helpful being in this using block,
                // so likely you'll create the scope at app startup, keep it
                // around during the app lifetime, and dispose of it manually
                // yourself during app shutdown.
                var serviceProvider = new AutofacServiceProvider(scope);

                var emptyService = serviceProvider.GetService<EmptyService>();
                var emptyService2 = serviceProvider.GetService<EmptyService>();
                Console.WriteLine(emptyService);
                Console.WriteLine(emptyService2);
            }
            var emptyService3 = rootProvider.GetService<EmptyService>();
            Console.WriteLine(emptyService3);
        }
    }
}
