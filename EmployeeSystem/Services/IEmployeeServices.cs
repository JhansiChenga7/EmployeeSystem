using EmployeeService.Models;
using EmployeeTenantResolver.Models;

namespace EmployeeService.Services
{
	public interface IEmployeeServices
	{
		Employee GetEmployeeByUserId(string tenantId);
		List<Employee> GetAllEmployees();
	}
}