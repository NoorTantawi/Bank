using Hope.infrastructure.Base;
using Hope.infrastructure.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Hope.UI.Controllers
{
    public class EmployeeController : BaseController
    {
        public EmployeeController(IConfiguration configuration) : base(configuration)
        {

        }
        public async Task<IActionResult> Create()
        {
            //api/Employee/GetAllQualification

            string url = configuration.GetSection("APIUrl").Value.ToString();
            HttpClient client = new HttpClient();

            var response = await client.GetAsync(url + "api/Employee/GetAllQualification");

            string apiResponse = await response.Content.ReadAsStringAsync();

            ViewBag.Qualification = JsonConvert.DeserializeObject<List<QualificationDTO>>(apiResponse);

            return View();
        }

        public async Task<IActionResult> AddNewEmployee(Hope.infrastructure.DTO.EmployeeDTO employeeDTO) 
        {
            HttpClient client = new HttpClient();
            string url = configuration.GetSection("APIUrl").Value.ToString();

            var EmployeeContextDTO = JsonConvert.SerializeObject(employeeDTO);

            var response = await client.PostAsync(url + "api/Employee/AddNewEmployee",
                new StringContent(EmployeeContextDTO, Encoding.UTF8, "application/json"));

            if(response.StatusCode == System.Net.HttpStatusCode.OK)
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
            try
            {
                HttpClient client = new HttpClient();
                string url = configuration.GetSection("APIUrl").Value.ToString();

                var response = await client.GetAsync(url + "api/Employee/GetAllEmployee");

                string apiResponse = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<List<EmployeeDTO>>(apiResponse);

                return View(result);

            }
            catch (Exception ex)
            {
                return View("~/Views/Home/ErrorPage.cshtml");
            }
        }
        public async Task <IActionResult> Update(int ID)
        {
            string url = configuration.GetSection("APIUrl").Value.ToString();
            HttpClient client = new HttpClient();

            var Qualificationresponse = await client.GetAsync(url + "api/Employee/GetAllQualification");

            string QualificationapiResponse = await Qualificationresponse.Content.ReadAsStringAsync();

            ViewBag.Qualification = JsonConvert.DeserializeObject<List<QualificationDTO>>(QualificationapiResponse);



            var response = await client.GetAsync(url + "api/Employee/GetEmployeeById?id=" + ID);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var apiResponse = await response.Content.ReadAsStringAsync();
                EmployeeDTO employeeDTO = JsonConvert.DeserializeObject<EmployeeDTO>(apiResponse);
                return View(employeeDTO);
            }
            else
            {
                return View();
            }
        }

        public async Task<IActionResult> Delete(int ID)
        {
            string url = configuration.GetSection("APIUrl").Value.ToString();
            HttpClient client = new HttpClient();

            var response = await client.GetAsync(url + "api/Employee/DeleteEmployee?id=" + ID);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("index");
            }
            else
            {
                return View();
            }

        }
        public async Task<IActionResult> UpdateEmployee(Hope.infrastructure.DTO.EmployeeDTO employeeDTO)
        {
            HttpClient client = new HttpClient();
            string url = configuration.GetSection("APIUrl").Value.ToString();

            var EmployeeContextDTO = JsonConvert.SerializeObject(employeeDTO);

            var response = await client.PostAsync(url + "api/Employee/UpdateEmployee",
                new StringContent(EmployeeContextDTO, Encoding.UTF8, "application/json"));

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
