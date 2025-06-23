using Hope.infrastructure.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Hope.UI.Controllers
{
    public class LoanController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Create()
        {

            string url = "http://localhost:37075/";
            HttpClient client = new HttpClient();

            var response = await client.GetAsync(url + "api/Client/GetAllClient");

            string apiResponse = await response.Content.ReadAsStringAsync();

            ViewBag.Clients = JsonConvert.DeserializeObject<List<ClientDTO>>(apiResponse);



            var responseLoan = await client.GetAsync(url + "api/Loan/GetAllLoanType");

            string apiResponseLoan = await responseLoan.Content.ReadAsStringAsync();

            ViewBag.LoanType = JsonConvert.DeserializeObject<List<LoanTypeDTO>>(apiResponseLoan);


            return View();
        }

        public async Task<IActionResult> AddNewLoan(LoanDTO loanDTO)
        {
            HttpClient client = new HttpClient();
            string url = "http://localhost:37075/";

                var LoanContextDTO = JsonConvert.SerializeObject(loanDTO);

                var response = await client.PostAsync(url + "api/Loan/AddNewLoan",
                    new StringContent(LoanContextDTO, Encoding.UTF8, "application/json"));

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return RedirectToAction("Create");
                }
                else
                {
                    return View("~/Views/Home/ErrorPage.cshtml");
                }



        }
    }
}
