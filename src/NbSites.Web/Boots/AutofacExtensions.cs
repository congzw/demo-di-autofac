using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;

namespace NbSites.Web.Boots
{
    public static class AutofacExtensions
    {
        public static IWebHostBuilder UseMyAutofac(this IWebHostBuilder builder)
        {
            return builder.ConfigureServices(sp =>
                {
                    //AddAutofac() is a convenience method for sp.AddSingleton<IServiceProviderFactory<ContainerBuilder>>(new AutofacServiceProviderFactory())
                    sp.AddAutofac();
                });
        }
    }
}