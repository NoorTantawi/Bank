using Hope.infrastructure.DTO;
using Hope.Repositories.IRepository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Hope.API.Controllers.Common
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CommonController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IAccountOpeningRepository _accountOpeningRepository;
        private readonly ILoanRepository _loanRepository;
        public CommonController(IEmployeeRepository employeeRepository, IClientRepository clientRepository, IAccountOpeningRepository accountOpeningRepository, ILoanRepository loanRepository)
        {
            _employeeRepository = employeeRepository;
            _clientRepository = clientRepository;
            _accountOpeningRepository = accountOpeningRepository;
            _loanRepository = loanRepository;
        }
        public IActionResult FillDashboard()
        {
            DashboardDTO dashboardDTO = new DashboardDTO();
            dashboardDTO.NumberOfAccountOpenings = _accountOpeningRepository.GetAll().Count();
            dashboardDTO.NumberOfClients = _clientRepository.GetAll().Count();
            dashboardDTO.NumberOfEmployees = _employeeRepository.GetAll().Count();
            dashboardDTO.NumberOfLoans = _loanRepository.GetAll().Count();

            string jsonString = JsonConvert.SerializeObject(dashboardDTO, Formatting.None, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            });

            return Ok(jsonString);

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
