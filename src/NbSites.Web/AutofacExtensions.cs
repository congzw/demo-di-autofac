using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace NbSites.Web
{
    public static class AutofacExtensions
    {
        //public static IWebHostBuilder UseMyAutofac(this IWebHostBuilder builder)
        //{
        //    return builder.UseAutofacMultitenantRequestServices()
        //        .ConfigureServices(sp =>
        //        {
        //            //AddAutofac() is a convenience method for sp.AddSingleton<IServiceProviderFactory<ContainerBuilder>>(new AutofacServiceProviderFactory())
        //            sp.AddAutofac();
        //            sp.AddSingleton<IServiceProviderFactory<ContainerBuilder>>(new AutofacMultitenantServiceProviderFactory(MultiTenantContainerSetup.Config));
        //        });
        //}

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