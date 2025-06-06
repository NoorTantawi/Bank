using Hope.DomainEntities.DBEntities;
using Hope.infrastructure.DTO;
using Hope.Repositories.IRepository;
using Hope.Repositories.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace Hope.API.Controllers.AccountOpening
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountOpeningController : Controller
    {

        private readonly IClientRepository _clientRepository;
        private readonly IAccountTypeRepository _accountTypeRepository;

        public AccountOpeningController(IClientRepository clientRepository, IAccountTypeRepository accountTypeRepository)
        {
            _clientRepository = clientRepository;
            _accountTypeRepository = accountTypeRepository;
        }

        public IActionResult GetAllClient()
        {
            List<ClientDTO> lstData = new List<ClientDTO>();

            lstData = (from x in _clientRepository.GetAll()
                       select new ClientDTO
                       {
                           ClientId = x.ClientId,
                           FullName = x.FullName,
                       }).ToList();

            string jsonString = JsonConvert.SerializeObject(lstData, Formatting.None, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            });

            return Ok(jsonString);
        }

        public IActionResult GetAllAccountType()
        {
            List<AccountTypeDTO> lstData = new List<AccountTypeDTO>();

            lstData = (from x in _accountTypeRepository.GetAll()
                       select new AccountTypeDTO
                       {
                           AccountTypeId = x.AccountTypeId,
                           AccountName = x.AccountName,
                       }).ToList();

            string jsonString = JsonConvert.SerializeObject(lstData, Formatting.None, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            });

            return Ok(jsonString);
        }
    }
}
