using Hope.infrastructure.DTO;
using Hope.Repositories.IRepository;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Hope.API.Controllers.ErrorLog
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ErrorLogController : Controller
    {
        private readonly IErrorLogRepository _errorLogRepository;

        public ErrorLogController(IErrorLogRepository errorLogRepository)
        {
            _errorLogRepository = errorLogRepository;
        }

        public IActionResult AddNewError(ErrorLogDTO errorLogDTO)
        {
            DomainEntities.DBEntities.ErrorLog errorLog = new DomainEntities.DBEntities.ErrorLog
            {
                ErrorMessage = errorLogDTO.ErrorMessage,
                ErrorDate = DateTime.Now,
                ModuleName = errorLogDTO.ModuleName
            };

            _errorLogRepository.Add(errorLog);

            return Ok("");
        }
    }
}
