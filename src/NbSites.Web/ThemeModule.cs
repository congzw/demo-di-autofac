using Autofac;
using Demo.Libs.Foos;
using Demo.Libs.MultiTenants;
using Demo.Libs.MultiThemes;
using NbSites.Web.Boots;

namespace NbSites.Web
{
    public class FooModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ThemeContextService>().As<IThemeContextService>();
            builder.RegisterType<TenantContextService>().As<ITenantContextService>();

            builder.RegisterType<FooService>().As<IFooService>().AsSelf().SingleInstance().Keyed<IFooService>("");
            builder.RegisterType<Foo1Service>().As<IFooService>().AsSelf().SingleInstance().Keyed<IFooService>("foo1");
            builder.RegisterType<Foo2Service>().As<IFooService>().AsSelf().SingleInstance().Keyed<IFooService>("foo2");
            builder.RegisterType<Foo3Service>().As<IFooService>().AsSelf().SingleInstance().Keyed<IFooService>("foo3");

            //? + ? => foo
            //? + a => foo1
            //? + b => foo2
            //? + c => foo3
            //? + not-exist => foo

            //1 + ? => foo1
            //1 + a => foo1
            //1 + b => foo1
            //1 + c => foo1
            //1 + not-exist => foo1

            //2 + ? => foo2
            //2 + a => foo2
            //2 + b => foo2
            //2 + c => foo2
            //2 + not-exist => foo2


            //not-exist + ? => foo
            //not-exist + a => foo1
            //not-exist + b => foo2
            //not-exist + c => foo3
            //not-exist + not-exist => foo

            builder.RegisterContextServices<IFooService>(cfg =>
            {
                //todo: load from config
                cfg.SetTenantServiceKey("1", "foo1");
                cfg.SetTenantServiceKey("2", "foo2");

                cfg.SetThemeServiceKey("a", "foo1");
                cfg.SetThemeServiceKey("b", "foo2");
                cfg.SetThemeServiceKey("c", "foo3");
            });

            #region or 

            //var serviceKeyRegistry = new ServiceKeyRegistry<IFooService>();

            //serviceKeyRegistry.SetThemeServiceKey("a", "foo1");
            //serviceKeyRegistry.SetThemeServiceKey("b", "foo2");
            //serviceKeyRegistry.SetThemeServiceKey("c", "foo3");

            //serviceKeyRegistry.SetTenantServiceKey("1", "foo1");
            //serviceKeyRegistry.SetTenantServiceKey("2", "foo2");

            //builder.RegisterInstance(serviceKeyRegistry);
            //builder.Register(ctx => ctx.GetServiceByContext<IFooService>());

            #endregion

        }
    }
}