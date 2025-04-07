using EmployeeService.Models;

namespace EmployeeService.Repository
{
	public class EmployeeRepository : IEmployeeRepository
	{
		private readonly Dictionary<string, Employee> _employeeData = new()
		{
			{ "100", new Employee() {  Id = 1, Name = "Alice", Department = "HR", Position = "Manager", UserId = "100" } } ,
			{ "200", new Employee() { Id = 2, Name = "Bob", Department = "IT", Position = "Developer", UserId = "200" } } ,
			{ "300", new Employee()  { Id = 3, Name = "Charlie", Department = "Finance", Position = "Analyst", UserId = "300"  } }
		};

		public Employee GetEmployeeByUserId(string userId)
		{
			if (_employeeData.ContainsKey(userId))
			{
				return _employeeData[userId];
			}
			return null;
		}
		public List<Employee> GetAllEmployees()
		{
			return _employeeData.Values.ToList();
		}
	}
}
