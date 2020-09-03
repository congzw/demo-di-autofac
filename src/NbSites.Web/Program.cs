using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace NbSites.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {

            //AddAutofac() is a convenience method for
            //services.AddSingleton<IServiceProviderFactory<ContainerBuilder>>(new AutofacServiceProviderFactory())
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseMyAutofac();
        }
    }
}
