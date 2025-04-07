using EmployeeTenantResolver.Models;

namespace EmployeeTenantResolver.Services
{
	public interface ITenantResolver
	{
		TenantInfo ResolveTenantInfo(string tenantId);
	}
}