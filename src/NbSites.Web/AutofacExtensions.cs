using Autofac;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace NbSites.Web
{
    public static class AutofacExtensions
    {
        public static IWebHostBuilder UseMyAutofac(this IWebHostBuilder builder)
        {
            return builder.UseAutofacMultitenantRequestServices()
                .ConfigureServices(sp =>
                {
                    //AddAutofac() is a convenience method for services.AddSingleton<IServiceProviderFactory<ContainerBuilder>>(new AutofacServiceProviderFactory())
                    //sp.AddAutofac();
                    //sp.AddSingleton<IServiceProviderFactory<ContainerBuilder>>(new AutofacServiceProviderFactory());
                    sp.AddSingleton<IServiceProviderFactory<ContainerBuilder>>(new AutofacMultitenantServiceProviderFactory(MultiTenantContainerSetup.Config));
                });
        }
    }
}