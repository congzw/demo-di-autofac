using Autofac;
using Autofac.Multitenant;
using Demo.Libs.Foos;
using Demo.Libs.MultiTenants;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace NbSites.Web
{
    public static class MultiTenantContainerSetup
    {
        public static MultitenantContainer ConfigureMultiTenantContainer(IContainer container)
        {
            //var strategy = new QueryStringTenantIdentificationStrategy(container.Resolve<IHttpContextAccessor>());
            var strategy = new MyTenantStrategy(container.Resolve<ITenantContextService>());

            var mtc = new MultitenantContainer(strategy, container);

            mtc.ConfigureTenant(null,
                b =>
                {
                    b.RegisterType<FooService>().As<IFooService>().SingleInstance();
                });

            mtc.ConfigureTenant("1",
                b =>
                {
                    b.RegisterType<Foo1Service>().As<IFooService>().SingleInstance();
                });

            mtc.ConfigureTenant("2",
                b =>
                {
                    b.RegisterType<Foo2Service>().As<IFooService>().SingleInstance();
                });


            return mtc;
        }
    }
    
    public class MyTenantStrategy : ITenantIdentificationStrategy
    {
        private readonly ITenantContextService _tenantContextService;

        public MyTenantStrategy(ITenantContextService tenantContextService)
        {
            _tenantContextService = tenantContextService;
        }

        public bool TryIdentifyTenant(out object tenantId)
        {
            tenantId = _tenantContextService.GetCurrentTenantId();

            var allTenantKeys = _tenantContextService.GetAllTenantIds();
            if (tenantId == null)
            {
                return false;
            }

            if (!allTenantKeys.Contains(tenantId))
            {
                return false;
            }

            return true;
        }
    }

    public static class MultiTenantExtensions
    {
        public static IWebHostBuilder UseMultiTenant(this IWebHostBuilder builder)
        {
            return builder.UseAutofacMultitenantRequestServices()
                .ConfigureServices(sp =>
                {
                    sp.AddSingleton<IServiceProviderFactory<ContainerBuilder>>(
                        new AutofacMultitenantServiceProviderFactory(
                            MultiTenantContainerSetup.ConfigureMultiTenantContainer));
                });
        }
    }
}