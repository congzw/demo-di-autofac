using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Demo.Libs;
using Microsoft.Extensions.DependencyInjection;

namespace DemoApp
{
    public class BasicDemo
    {
        public static void Run()
        {
            // The Microsoft.Extensions.DependencyInjection.ServiceCollection
            // has extension methods provided by other .NET Core libraries to
            // regsiter services with DI.
            var serviceCollection = new ServiceCollection();

            //// The Microsoft.Extensions.Logging package provides this one-liner
            //// to add logging services.
            //serviceCollection.AddLogging();

            var containerBuilder = new ContainerBuilder();

            // Once you've registered everything in the ServiceCollection, call
            // Populate to bring those registrations into Autofac. This is
            // just like a foreach over the list of things in the collection
            // to add them to Autofac.
            containerBuilder.Populate(serviceCollection);

            // Make your Autofac registrations. Order is important!
            // If you make them BEFORE you call Populate, then the
            // registrations in the ServiceCollection will override Autofac
            // registrations; if you make them AFTER Populate, the Autofac
            // registrations will override. You can make registrations
            // before or after Populate, however you choose.
            containerBuilder.RegisterType<HelloService>().As<IHelloService>();

            // Creating a new AutofacServiceProvider makes the container
            // available to your app using the Microsoft IServiceProvider
            // interface so you can use those abstractions rather than
            // binding directly to Autofac.
            var container = containerBuilder.Build();
            var serviceProvider = new AutofacServiceProvider(container);


            var helloService = serviceProvider.GetService<IHelloService>();

            Console.WriteLine(helloService.Hello());
        }
    }
}
