using System.Collections.Generic;

namespace Demo.Libs.MultiTenants
{
    public class TenantIdRepository : ITenantIdRepository
    {

        public IList<object> GetAllTenantIds()
        {
            return new List<object>() { "1", "2" };
        }
    }
}