using Autofac;
using Autofac.Multitenant;
using Demo.Libs.Bars;
using Demo.Libs.MultiTenants;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace NbSites.Web.Boots
{
    //多租户的示例，暂时不需要

    public static class MultiTenantExtensions
    {
        // Use Autofac Multi Tenant DEMO
        public static IWebHostBuilder UseMyAutofacMultiTenant(this IWebHostBuilder builder)
        {
            return builder.UseAutofacMultitenantRequestServices()
                .ConfigureServices(sp =>
                {
                    sp.AddSingleton<IServiceProviderFactory<ContainerBuilder>>(new AutofacMultitenantServiceProviderFactory(MultiTenantContainerSetup.Config));
                });
        }

    }

    public static class MultiTenantContainerSetup
    {
        public static MultitenantContainer Config(IContainer container)
        {
            var strategy = new MyTenantStrategy(container.Resolve<ITenantContextService>(), container.Resolve<ITenantIdRepository>());

            var mtc = new MultitenantContainer(strategy, container);

            mtc.ConfigureTenant(null, cfg =>
            {
                cfg.RegisterType<BarService>().As<IBarService>().SingleInstance();
            });

            mtc.ConfigureTenant("1", cfg =>
            {
                cfg.RegisterType<Bar1Service>().As<IBarService>().SingleInstance();
            });

            mtc.ConfigureTenant("2", cfg =>
            {
                cfg.RegisterType<Bar2Service>().As<IBarService>().SingleInstance();
            });

            return mtc;
        }
    }

    public class MyTenantStrategy : ITenantIdentificationStrategy
    {
        private readonly ITenantContextService _tenantContextService;
        private readonly ITenantIdRepository _tenantIdRepository;

        public MyTenantStrategy(ITenantContextService tenantContextService, ITenantIdRepository tenantIdRepository)
        {
            _tenantContextService = tenantContextService;
            _tenantIdRepository = tenantIdRepository;
        }

        public bool TryIdentifyTenant(out object tenantId)
        {
            tenantId = _tenantContextService.GetCurrentTenantId();

            var allTenantKeys = _tenantIdRepository.GetAllTenantIds();
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
}