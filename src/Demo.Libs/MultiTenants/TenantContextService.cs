using Microsoft.AspNetCore.Http;

namespace Demo.Libs.MultiTenants
{
    public class TenantContextService : ITenantContextService
    {
        private readonly IHttpContextAccessor _accessor;

        public TenantContextService(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public object GetCurrentTenantId()
        {
            var context = _accessor.HttpContext;
            if (context == null)
            {
                return null;
            }

            if (context.Items.TryGetValue("_tenantId", out var tenantId))
            {
                return tenantId;
            }

            if (context.Request.Query.TryGetValue("tenant", out var tenantValues))
            {
                //NULL OR EMPTY
                if (string.IsNullOrWhiteSpace(tenantValues[0]))
                {
                    context.Items["_tenantId"] = null;
                    return null;
                }

                tenantId = tenantValues[0];
                context.Items["_tenantId"] = tenantId;
                return tenantId;
            }

            return null;
        }
    }
}
