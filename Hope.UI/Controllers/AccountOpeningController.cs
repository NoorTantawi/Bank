using Hope.infrastructure.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
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

        public async Task<IActionResult> CheckIfUserHasAccount(int AccountTypeId, int ClientId)
        {
            string url = "http://localhost:37075/";
            HttpClient client = new HttpClient();

            var response = await client.GetAsync(url + "api/AccountOpening/CheckIfUserHasAccount?AccountTypeId="
                + AccountTypeId + "&ClientId=" + ClientId);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var apiResponse = await response.Content.ReadAsStringAsync();
                //string status = JsonConvert.DeserializeObject<string>(apiResponse);
                return Json(apiResponse);
            }
            else
            {
                return View();
            }

        }

        public async Task<IActionResult> AddNewAccountOpening(Hope.infrastructure.DTO.AccountOpeningDTO accountOpeningDTO)
        {
            HttpClient client = new HttpClient();
            string url = "http://localhost:37075/";

            var AccountOpeningContextDTO = JsonConvert.SerializeObject(accountOpeningDTO);

            var response = await client.PostAsync(url + "api/AccountOpening/AddNewAccountOpening",
                new StringContent(AccountOpeningContextDTO, Encoding.UTF8, "application/json"));

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return View();
            }
            else
            {
                return View("~/Views/Home/ErrorPage.cshtml");
                //Error Page
            }
        }
    }
}
