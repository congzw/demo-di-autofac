using System.Collections.Generic;

namespace Demo.Libs.MultiTenants
{
    public interface ITenantContextService
    {
        //todo: refactor to another interface
        IList<object> GetAllTenantIds();
        object GetCurrentTenantId();
    }
}
