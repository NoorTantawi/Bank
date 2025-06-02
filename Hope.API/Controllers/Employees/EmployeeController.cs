using Hope.Repositories.IRepository;
using Hope.Repositories.Repository;
using Microsoft.AspNetCore.Mvc;
using Hope.infrastructure.DTO;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using Hope.DomainEntities.DBEntities;
using System;



namespace Hope.API.Controllers.Employees
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IQualificationRepository _qualificationRepository;

        public EmployeeController(IEmployeeRepository employeeRepository, IQualificationRepository qualificationRepository)
        {
            _employeeRepository = employeeRepository;
            _qualificationRepository = qualificationRepository;
        }

        public IActionResult AddNewEmployee(EmployeeDTO employeeDTO)
        {
            DomainEntities.DBEntities.Employee employee = new DomainEntities.DBEntities.Employee();

            employee.Address = employeeDTO.Address;
            employee.DateOfBirth = employeeDTO.DateOfBirth;
            employee.Email = employeeDTO.Email;
            employee.FullName = employeeDTO.FullName;
            employee.JoinDate = employeeDTO.JoinDate;
            employee.Mobile = employeeDTO.Mobile;
            employee.QualificationId = employeeDTO.QualificationId;
            employee.Salary = employeeDTO.Salary;

            _employeeRepository.Add(employee);

            return Ok("Success");

        }
        public IActionResult GetEmployeeById(int Id)
        {
            DomainEntities.DBEntities.Employee employee = new DomainEntities.DBEntities.Employee();
            employee = _employeeRepository.GetById(Id);

            EmployeeDTO employeeDTO = new EmployeeDTO();
            employeeDTO.Address = employee.Address;
            employeeDTO.DateOfBirth = employee.DateOfBirth;
            employeeDTO.Email = employee.Email;
            employeeDTO.FullName = employee.FullName;
            employeeDTO.JoinDate = employee.JoinDate;
            employeeDTO.Mobile = employee.Mobile;
            employeeDTO.QualificationId = employee.QualificationId;
            employeeDTO.Salary = employee.Salary;
            employeeDTO.EmployeeId = employee.EmployeeId;

            return Ok(employeeDTO);
        }

        public IActionResult UpdateEmployee(EmployeeDTO employeeDTO)
        {
            DomainEntities.DBEntities.Employee employee = new DomainEntities.DBEntities.Employee();

            employee = _employeeRepository.GetById(employeeDTO.EmployeeId); //GetAll().Where(x => x.EmployeeId == employeeDTO.EmployeeId).FirstOrDefault();

            employee.Address = employeeDTO.Address;
            employee.DateOfBirth = employeeDTO.DateOfBirth;
            employee.Email = employeeDTO.Email;
            employee.FullName = employeeDTO.FullName;
            employee.JoinDate = employeeDTO.JoinDate;
            employee.Mobile = employeeDTO.Mobile;
            employee.QualificationId = employeeDTO.QualificationId;
            employee.Salary = employeeDTO.Salary;

            _employeeRepository.Update(employee);

            return Ok("Success");
        }

        public IActionResult DeleteEmployee(int Id)
        {
            try
            {
                DomainEntities.DBEntities.Employee employee = new DomainEntities.DBEntities.Employee();

                employee = _employeeRepository.GetById(Id);

                _employeeRepository.Delete(employee);

                return Ok("Success");
            }
            catch (Exception ex)
            {
                return Ok("False");
            }
        }

        public IActionResult GetAllEmployee()
        {
            List<EmployeeDTO> lstData = (from obj in _employeeRepository.GetAll().AsQueryable()
                                         select new EmployeeDTO
                                         {
                                             EmployeeId = obj.EmployeeId,
                                             Address = obj.Address,
                                             DateOfBirth = obj.DateOfBirth,
                                             Email = obj.Email,
                                             FullName = obj.FullName,
                                             JoinDate = obj.JoinDate,
                                             Mobile = obj.Mobile,
                                             QualificationId = obj.QualificationId,
                                             Salary = obj.Salary,
                                         }).ToList();

            string jsonString = JsonConvert.SerializeObject(lstData, Formatting.None, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            });

            return Ok(jsonString);
                
        }
        public IActionResult GetAllQualification()
        {
            List<QualificationDTO> lst = (from obj in _qualificationRepository.GetAll()
                                          select new QualificationDTO
                                          {
                                              Id = obj.QualificationId,
                                              Name = obj.QualificationName
                                          }).ToList();


            string jsonString = JsonConvert.SerializeObject(lst, Formatting.None, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            });

            return Ok(jsonString);
        }
        
    }

}
