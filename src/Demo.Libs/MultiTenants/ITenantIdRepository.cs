using System.Collections.Generic;

namespace Demo.Libs.MultiTenants
{
    public interface ITenantIdRepository
    {
        IList<object> GetAllTenantIds();
    }
}