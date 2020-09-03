using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Autofac;
using Autofac.Builder;
using Demo.Libs.MultiTenants;
using Demo.Libs.MultiThemes;

namespace NbSites.Web.Boots
{
    public static class KeyedServiceResolveExtensions
    {
        public static IRegistrationBuilder<T, SimpleActivatorData, SingleRegistrationStyle> RegisterContextServices<T>(this ContainerBuilder builder, Action<ServiceKeyRegistry<T>> config)
        {
            var serviceKeyRegistry = new ServiceKeyRegistry<T>();
            config(serviceKeyRegistry);
            builder.RegisterInstance(serviceKeyRegistry);
            return builder.Register(ctx => ctx.GetServiceByContext<T>());
        }

        public static TService GetServiceByContext<TService>(this IComponentContext ctx)
        {
            var serviceKey = ctx.GetServiceKey<TService>();
            return ctx.ResolveKeyed<TService>(serviceKey);
        }

        public static string GetServiceKey<TService>(this IComponentContext ctx)
        {
            //定制的优先级： 租户 > 主题 > 默认
            //tenant? => theme? => ""

            var serviceKeyRegistry = ctx.Resolve<ServiceKeyRegistry<TService>>();

            //tenant? 
            var tenantContextService = ctx.Resolve<ITenantContextService>();
            var tenantId = tenantContextService.GetCurrentTenantId();
            var tenantIdFix = tenantId == null ? string.Empty : tenantId.ToString();
            var tenantServiceKey = serviceKeyRegistry.LookupTenantServiceKey(tenantIdFix);
            if (!string.IsNullOrWhiteSpace(tenantServiceKey))
            {
                return tenantServiceKey;
            }

            //theme? 
            var themeContextService = ctx.Resolve<IThemeContextService>();
            var theme = themeContextService.GetCurrentTheme();
            var themeFix = theme ?? string.Empty;
            var themeServiceKey = serviceKeyRegistry.LookupThemeServiceKey(themeFix);
            return themeServiceKey;
        }
    }

    public class ServiceKeyRegistry<TService>
    {
        internal IDictionary<string, string> TenantLocates { get; set; } =
            new ConcurrentDictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        internal IDictionary<string, string> ThemeLocates { get; set; } =
            new ConcurrentDictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        public ServiceKeyRegistry<TService> SetTenantServiceKey(string tenant, string serviceKey)
        {
            TenantLocates[tenant] = serviceKey;
            return this;
        }

        public ServiceKeyRegistry<TService> SetThemeServiceKey(string theme, string serviceKey)
        {
            ThemeLocates[theme] = serviceKey;
            return this;
        }

        public string LookupTenantServiceKey(string tenant)
        {
            return TenantLocates.TryGetValue(tenant, out var tenantFind) ? tenantFind : string.Empty;
        }

        public string LookupThemeServiceKey(string theme)
        {
            return ThemeLocates.TryGetValue(theme, out var themeFind) ? themeFind : string.Empty;
        }
    }
}