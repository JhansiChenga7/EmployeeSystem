using EmployeeTenantResolver.Models;
using EmployeeService.Models;

namespace EmployeeUI.ViewModels
{
	public class EmployeeDetailsViewModel
	{
		public List<Employee> employee { get; set; }
		public TenantInfo tenantInfo { get; set; }
	}
}
