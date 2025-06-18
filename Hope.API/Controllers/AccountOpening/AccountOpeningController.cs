using Hope.DomainEntities.DBEntities;
using Hope.infrastructure.DTO;
using Hope.Repositories.IRepository;
using Hope.Repositories.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace Hope.API.Controllers.AccountOpening
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountOpeningController : Controller
    {

        private readonly IClientRepository _clientRepository;
        private readonly IAccountTypeRepository _accountTypeRepository;
        private readonly IAccountOpeningRepository _accountOpeningRepository;
        private readonly IErrorLogRepository _errorLogRepository;

        public AccountOpeningController(IClientRepository clientRepository, IAccountTypeRepository accountTypeRepository, IAccountOpeningRepository accountOpeningRepository, IErrorLogRepository errorLogRepository)
        {
            _clientRepository = clientRepository;
            _accountTypeRepository = accountTypeRepository;
            _accountOpeningRepository = accountOpeningRepository;
            _errorLogRepository = errorLogRepository;
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

        public IActionResult CheckIfUserHasAccount(int AccountTypeId, int ClientId)
        {
            int count = 0;
            count = _accountOpeningRepository.Find(obj => obj.AccountTypeId == AccountTypeId && obj.ClientId == ClientId).Count();

            if (count > 0)
            {
                return Ok("Success");// User already has an account of this type.
            }
            else
            {
                return Ok("Fail"); // Can open a new account
            }

        }

        public IActionResult AddNewAccountOpening(AccountOpeningDTO accountOpeningDTO)
        {
            try
            {
                DomainEntities.DBEntities.AccountOpening accountOpening = new DomainEntities.DBEntities.AccountOpening();

                accountOpening.AccountTypeId = accountOpeningDTO.AccountTypeId;
                accountOpening.ClientId = accountOpeningDTO.ClientId;
                accountOpening.Currency = accountOpeningDTO.Currency;
                accountOpening.OpeningDate = DateTime.Now;
                accountOpening.Balance = accountOpeningDTO.Balance;
                accountOpening.Iban = accountOpeningDTO.Iban;
                accountOpening.Status = true; // Assuming the account is active upon creation

                _accountOpeningRepository.Add(accountOpening);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                DomainEntities.DBEntities.ErrorLog errorLog = new DomainEntities.DBEntities.ErrorLog();
                if (ex.InnerException != null)
                {
                    errorLog.ErrorMessage = ex.InnerException.ToString();
                }
                else
                {
                    errorLog.ErrorMessage = ex.Message;
                }
                errorLog.ModuleName = "API Add New Account Opening";
                errorLog.ErrorDate = DateTime.Now;

                _errorLogRepository.Add(errorLog);



                return BadRequest("Fail");
            }
        }
    }
}
