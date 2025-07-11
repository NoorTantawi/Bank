using Hope.infrastructure.DTO;
using Hope.Repositories.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
        private readonly IModuleRepository _moduleRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IRoleModuleRepository _roleModuleRepository;
        private readonly IRoleUserRepository _roleUserRepository;
        public CommonController(IEmployeeRepository employeeRepository, IClientRepository clientRepository, 
            IAccountOpeningRepository accountOpeningRepository, ILoanRepository loanRepository, 
            IModuleRepository moduleRepository, IRoleRepository roleRepository, 
            IRoleModuleRepository roleModuleRepository, IRoleUserRepository roleUserRepository)
        {
            _employeeRepository = employeeRepository;
            _clientRepository = clientRepository;
            _accountOpeningRepository = accountOpeningRepository;
            _loanRepository = loanRepository;
            _moduleRepository = moduleRepository;
            _roleRepository = roleRepository;
            _roleModuleRepository = roleModuleRepository;
            _roleUserRepository = roleUserRepository;
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

        public IActionResult GetAllPermissionByUserId(int userId)
        {
            List<int> lstRoles = _roleUserRepository.Find(x => x.EmployeeId == userId).Select(x => x.RoleId).ToList();

            List<int> lstModules = _roleModuleRepository.Find(x => lstRoles.Contains(x.RoleId)).Select(x => x.ModuleId).Distinct().ToList();

            MenuPermissionDTO menuPermissionDTO = new MenuPermissionDTO();

            if (lstModules.Contains(1))
                menuPermissionDTO.Employees = "True";
            if (lstModules.Contains(2))
                menuPermissionDTO.Clients = "True";
            if (lstModules.Contains(3))
                menuPermissionDTO.Accounts = "True";
            if (lstModules.Contains(4))
                menuPermissionDTO.Loans = "True";

            return Ok(menuPermissionDTO);
        }
    }
}
