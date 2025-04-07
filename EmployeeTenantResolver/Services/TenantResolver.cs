using EmployeeTenantResolver.Models;

namespace EmployeeTenantResolver.Services
{
	public class TenantResolver : ITenantResolver
	{
		private readonly Dictionary<string, TenantInfo> _tenantData = new ()
		{
			{ "tenant1", new TenantInfo (){  tenantId = "tenant1",tenantUrl = "https://localhost:7283"} } ,
			{ "tenant2", new TenantInfo (){  tenantId = "tenant2",tenantUrl = "https://localhost:7284"} } ,
			{ "tenant3", new TenantInfo (){  tenantId = "tenant3",tenantUrl = "https://localhost:7285"} } 
		};

		public TenantInfo ResolveTenantInfo(string tenantId)
		{
			var response = _tenantData.ContainsKey(tenantId) ? _tenantData[tenantId] : new TenantInfo();
			return response;
		}


	}
}