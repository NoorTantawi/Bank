 using Microsoft.AspNetCore.Mvc;
namespace Hope.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TestController : Controller
    {
        [HttpGet]
        public IActionResult GetFullName()
        {
            return Ok("Nooraldeen Tantawi");
        }

        [HttpPost] // doesn't work
        
        public IActionResult AddNewUser(string FirstName, string LastName)
        {
            return Ok(FirstName + LastName);
        }

        public class Custom
        {
            public string FirstName { get; set; }    
            public string LastName { get; set; }
            public string Email { get; set; }
        }
    }
}
