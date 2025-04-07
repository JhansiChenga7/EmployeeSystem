using EmployeeService.Models;

namespace EmployeeService.Repository
{
	public interface IEmployeeRepository
	{
		Employee GetEmployeeByUserId(string userId);
		List<Employee> GetAllEmployees();
		
	}
}
