using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
