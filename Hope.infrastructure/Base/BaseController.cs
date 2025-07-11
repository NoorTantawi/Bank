using Hope.infrastructure.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Hope.infrastructure.Base
{
    public class BaseController : Controller
    {
        public readonly IConfiguration configuration;

        public BaseController(IConfiguration iconfiguration)
        {
            this.configuration = iconfiguration;
        }

        public override async void OnActionExecuting(ActionExecutingContext context)
        {

            if (IsUserLoggedIn())
            {
                ViewBag.UserId = HttpContext.User.Claims.Where(obj => obj.Type == "UserID").FirstOrDefault().Value;
            }
            else
            {
                context.Result = RedirectToAction("Login", "Account");
            }
        }

        public bool IsUserLoggedIn()
        {
            var result = HttpContext.User.Claims.Where(obj => obj.Type == "UserID").FirstOrDefault();
            if (result == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<IActionResult> GetAllPermissionByUserId()
        {
            int id = 0;
            if (ViewBag.UserId != null)
            {
                id = Convert.ToInt32(ViewBag.UserId);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

            HttpClient client = new HttpClient();
            string url = "http://localhost:37075/";

            var reponse = await client.GetAsync(url + "api/Common/GetAllPermissionByUserId?userId=" + id);

            MenuPermissionDTO menuPermissionDTO = new MenuPermissionDTO();

            if (reponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var Data = await reponse.Content.ReadAsStringAsync();
                menuPermissionDTO = JsonConvert.DeserializeObject<MenuPermissionDTO>(Data);
            }

            return PartialView("_ManageMenu", menuPermissionDTO);
        }
    }
    
}
