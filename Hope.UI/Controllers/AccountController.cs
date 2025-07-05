using Hope.infrastructure.Base;
using Hope.infrastructure.DTO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hope.UI.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult LoginUser(LoginDTO loginDTO)
        {
            //HttpContext.Response.Cookies.Append("Username", loginDTO.Username.ToString());
            var Item = LoginUserDetails(loginDTO).Result;

            if (Item != -1)
            {
                var userClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier,loginDTO.Username),
                    new Claim("UserName","Admin"),
                    new Claim("UserID",Item.ToString())
                };

                var userIdentity = new ClaimsIdentity(userClaims, "User Identity");
                var userPrincipal = new ClaimsPrincipal(new[] {userIdentity});
                HttpContext.SignInAsync(userPrincipal);

                return RedirectToAction("Index", "Home");
            }
            else 
            { 
                return View("Login"); 
            }

        }

        public async Task<int> LoginUserDetails(LoginDTO loginDTO)
        {
            HttpClient client = new HttpClient();
            string url = "http://localhost:37075/";

            var LoginContextDTO = JsonConvert.SerializeObject(loginDTO);

            var reponse = await client.PostAsync(url + "api/User/Login",
                new StringContent(LoginContextDTO, System.Text.Encoding.UTF8, "application/json"));

            var data = await reponse.Content.ReadAsStringAsync();
            int id = JsonConvert.DeserializeObject<int>(data);

            return id;
        }
    }
}
