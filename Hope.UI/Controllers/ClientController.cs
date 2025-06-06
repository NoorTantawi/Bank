using Hope.infrastructure.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Hope.UI.Controllers
{
        public class ClientController : Controller
        {
            public async Task<IActionResult> Create()
            {
                //api/Employee/GetAllQualification

                string url = "http://localhost:37075/";
                HttpClient client = new HttpClient();

                var response = await client.GetAsync(url + "api/Client/GetAllNationality");

                string apiResponse = await response.Content.ReadAsStringAsync();

                ViewBag.Nationality = JsonConvert.DeserializeObject<List<NationalityDTO>>(apiResponse);

                return View();
            }

            public async Task<IActionResult> AddNewClient(Hope.infrastructure.DTO.ClientDTO clientDTO)
            {
                HttpClient client = new HttpClient();
                string url = "http://localhost:37075/";

                var ClientContextDTO = JsonConvert.SerializeObject(clientDTO);

                var response = await client.PostAsync(url + "api/Client/AddNewClient",
                    new StringContent(ClientContextDTO, Encoding.UTF8, "application/json"));

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                    //Error Page
                }

            }

            public async Task<IActionResult> Index()
            {
                HttpClient client = new HttpClient();
                string url = "http://localhost:37075/";

                var response = await client.GetAsync(url + "api/Client/GetAllClient");

                string apiResponse = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<List<ClientDTO>>(apiResponse);

                return View(result);
            }
            public async Task<IActionResult> Update(int ID)
            {
                string url = "http://localhost:37075/";
                HttpClient client = new HttpClient();

                var Nationalityresponse = await client.GetAsync(url + "api/Client/GetAllNationality");

                string NationalityapiResponse = await Nationalityresponse.Content.ReadAsStringAsync();

                ViewBag.Nationality = JsonConvert.DeserializeObject<List<NationalityDTO>>(NationalityapiResponse);



                var response = await client.GetAsync(url + "api/Client/GetClientById?id=" + ID);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    ClientDTO clientDTO = JsonConvert.DeserializeObject<ClientDTO>(apiResponse);
                    return View(clientDTO);
                }
                else
                {
                    return View();
                }
            }

            public async Task<IActionResult> Delete(int ID)
            {
                string url = "http://localhost:37075/";
                HttpClient client = new HttpClient();

                var response = await client.GetAsync(url + "api/Client/DeleteClient?id=" + ID);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return RedirectToAction("index");
                }
                else
                {
                    return View();
                }

            }
            public async Task<IActionResult> UpdateClient(Hope.infrastructure.DTO.ClientDTO clientDTO)
            {
                HttpClient client = new HttpClient();
                string url = "http://localhost:37075/";

                var ClientContextDTO = JsonConvert.SerializeObject(clientDTO);

                var response = await client.PostAsync(url + "api/Client/UpdateClient",
                    new StringContent(ClientContextDTO, Encoding.UTF8, "application/json"));

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                    //Error Page
                }

            }

        }
}


