namespace Demo.Libs.MultiTenants
{
    public interface ITenantContextService
    {
        object GetCurrentTenantId();
    }
}
