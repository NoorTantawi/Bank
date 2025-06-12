using Hope.infrastructure.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hope.UI.Controllers
{
    public class AccountOpeningController : Controller
    {
        public async Task<IActionResult> Create()
        {

            string url = "http://localhost:37075/";
            HttpClient client = new HttpClient();

            var response = await client.GetAsync(url + "api/AccountOpening/GetAllClient");

            string apiResponse = await response.Content.ReadAsStringAsync();

            ViewBag.Clients = JsonConvert.DeserializeObject<List<ClientDTO>>(apiResponse);



            var responseType = await client.GetAsync(url + "api/AccountOpening/GetAllAccountType");

            string apiResponseType = await responseType.Content.ReadAsStringAsync();

            ViewBag.AccountTypes = JsonConvert.DeserializeObject<List<AccountTypeDTO>>(apiResponseType);


            return View();
        }

        public IActionResult CheckIfUserHasAccount(int AccountTypeId, int ClientId)
        {

            return Json("Success");
        }
    }
}
