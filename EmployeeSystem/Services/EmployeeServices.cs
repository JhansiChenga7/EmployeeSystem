
using EmployeeService.Models;
using EmployeeService.Repository;
using EmployeeService.Services;

namespace EmployeeService.Services
{
	public class EmployeeServices : IEmployeeServices
	{
		private readonly IEmployeeRepository _employeeRepository;

		public EmployeeServices(IEmployeeRepository employeeRepository)
		{
			_employeeRepository = employeeRepository;
		}

		public Employee GetEmployeeByUserId(string userId)
		{
			return _employeeRepository.GetEmployeeByUserId(userId);
		}

		public List<Employee> GetAllEmployees()
		{
			return _employeeRepository.GetAllEmployees();
		}
	}
}