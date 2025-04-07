using Microsoft.AspNetCore.Mvc;
using EmployeeService.Services;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class EmployeeController : ControllerBase
{
	private readonly IEmployeeServices _employeeService;

	public EmployeeController(IEmployeeServices employeeService)
	{
		_employeeService = employeeService;
	}

	[HttpGet("{userId}")]
	public IActionResult GetEmployeeByUserId(string userId)
	{
		var employees = _employeeService.GetEmployeeByUserId(userId);
		return Ok(employees);
	}

	[HttpGet]
	public IActionResult GetAllEmployees()
	{
		var employees = _employeeService.GetAllEmployees();
		return Ok(employees);
	}
}