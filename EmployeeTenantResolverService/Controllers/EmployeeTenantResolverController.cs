using EmployeeTenantResolver.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeTenantResolverService.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class EmployeeTenantResolverController : ControllerBase
	{
		private readonly ITenantResolver _tenantResolver;

		public EmployeeTenantResolverController(ITenantResolver tenantResolver)
		{
			_tenantResolver = tenantResolver;
		}

		[HttpGet("{tenantId}")]
		public IActionResult GetTenantInfo(string tenantId)
		{
			var tenantInfo = _tenantResolver.ResolveTenantInfo(tenantId);
			return Ok(tenantInfo);
		}
	}
}
