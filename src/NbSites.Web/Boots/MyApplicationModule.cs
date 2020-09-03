using Autofac;
using Demo.Libs;
using Demo.Libs.MultiTenants;
using Demo.Libs.MultiThemes;

namespace NbSites.Web.Boots
{
    public class MyApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<HelloService>().As<IHelloService>();
            builder.RegisterType<EmptyService>().SingleInstance().AsSelf();
            builder.RegisterType<ThemeContextService>().As<IThemeContextService>().InstancePerLifetimeScope();
            builder.RegisterType<TenantContextService>().As<ITenantContextService>().InstancePerLifetimeScope();
            builder.RegisterType<TenantIdRepository>().As<ITenantIdRepository>().SingleInstance();


        }
    }
}