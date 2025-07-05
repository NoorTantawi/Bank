using Hope.infrastructure.DTO;
using Hope.Repositories.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Hope.API.Controllers.Users
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        public UserController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public IActionResult Login(LoginDTO loginDTO)
        {
            var result = _employeeRepository.Find(x => x.Username == loginDTO.Username
            && x.Password == loginDTO.Password).FirstOrDefault();

            if (result != null)
            {
                return Ok(result.EmployeeId);
            }
            else
            {
                return BadRequest(-1);
            }

        }
    }
}
