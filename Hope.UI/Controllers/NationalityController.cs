using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using Hope.infrastructure.Base;
using Microsoft.Extensions.Configuration;


namespace Hope.UI.Controllers
{
    public class NationalityController : BaseController
    {
        public NationalityController(IConfiguration configuration) : base(configuration)
        {

        }
        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> AddNewNationality(Hope.infrastructure.DTO.NationalityDTO nationalityDTO) 
        {
            HttpClient client = new HttpClient();
            string url = configuration.GetSection("APIUrl").Value.ToString();

            var nationalityContextDTO = JsonConvert.SerializeObject(nationalityDTO);

            var response = await client.PostAsync(url + "api/Nationality/AddNewNationality",
                new StringContent(nationalityContextDTO, Encoding.UTF8, "application/json"));// media => application/json

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //Good;
            }
            else
            {
                //Error 
            }
            return View();
        }
    }
}
