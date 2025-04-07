using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
	[HttpPost("login")]
	public IActionResult Login([FromBody] LoginModel login)
	{
		if (login.Username == "admin" && login.Password == "password") // Simple validation
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.UTF8.GetBytes("SuperSecretKey12345SuperSecretKey12345");

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, login.Username) }),
				Expires = DateTime.UtcNow.AddHours(1),
				Issuer = "EmployeeService",
				Audience = "EmployeeUI",
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

			};

			var token = tokenHandler.CreateToken(tokenDescriptor);
			return Ok(tokenHandler.WriteToken(token));

		}

		return Unauthorized();
	}
}

public class LoginModel
{
	public string Username { get; set; }
	public string Password { get; set; }
}
