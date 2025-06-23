using Hope.infrastructure.DTO;
using Hope.Repositories.IRepository;
using Hope.Repositories.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Hope.API.Controllers.Loan
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LoanController : Controller
    {
        private readonly ILoanTypeRepository _loanTypeRepository;
        private readonly ILoanRepository _loanRepository;

        public LoanController(ILoanTypeRepository loanTypeRepository, ILoanRepository loanRepository)
        {
            _loanTypeRepository = loanTypeRepository;
            _loanRepository = loanRepository;
        }
        public IActionResult GetAllLoanType()
        {
            List<LoanTypeDTO> lst = (from obj in _loanTypeRepository.GetAll()
                                          select new LoanTypeDTO
                                          {
                                              LoanTypeId = obj.LoanTypeId,
                                              TypeName = obj.TypeName
                                          }).ToList();

            string jsonString = JsonConvert.SerializeObject(lst, Formatting.None, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            });

            return Ok(jsonString);
        }

        public IActionResult AddNewLoan(LoanDTO loanDTO)
        {
            DomainEntities.DBEntities.Loan loan = new DomainEntities.DBEntities.Loan();

            loan.ClientId = loanDTO.ClientId;
            loan.LoanTypeId = loanDTO.LoanTypeId;
            loan.InterestRate = loanDTO.InterestRate;
            loan.StartDate = loanDTO.StartDate;
            loan.EndDate = loanDTO.EndDate;
            loan.LoanPeriod = loanDTO.LoanPeriod;
            loan.LoanAmount = loanDTO.LoanAmount;
            loan.LoanSattelmentAmount = loanDTO.LoanSattelmentAmount;
            loan.TotalAmountwithInterest = loanDTO.TotalAmountwithInterest;
            loan.TaxValue = loanDTO.TaxValue;
            loan.Currency = loanDTO.Currency;

            _loanRepository.Add(loan);

            return Ok("Success");

        }

    }
}
