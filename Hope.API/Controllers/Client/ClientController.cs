﻿using Hope.infrastructure.DTO;
using Hope.Repositories.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace Hope.API.Controllers.Client
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ClientController : Controller
    {

        private readonly IClientRepository _clientRepository;
        private readonly INationalityRepository _nationalityRepository;

        public ClientController(IClientRepository clientRepository, INationalityRepository nationalityRepository)
        {
            _clientRepository = clientRepository;
            _nationalityRepository = nationalityRepository;
        }

        public IActionResult AddNewClient(ClientDTO clientDTO) 
        {
            DomainEntities.DBEntities.Client obj = new DomainEntities.DBEntities.Client();

            obj.Address = clientDTO.Address;
            obj.DateOfBirth = clientDTO.DateOfBirth;
            obj.Email = clientDTO.Email;
            obj.FullName = clientDTO.FullName;
            obj.Mobile = clientDTO.Mobile;
            obj.NationalId = clientDTO.NationalId;
            obj.NationalityId = clientDTO.NationalityId;
            obj.PassportNumber = clientDTO.PassportNumber;
            obj.RegisterDate = clientDTO.RegisterDate;

            _clientRepository.Add(obj);

            return Ok("Success");
        }

        public IActionResult UpdateClient(ClientDTO clientDTO)
        {
            DomainEntities.DBEntities.Client client = new DomainEntities.DBEntities.Client();

            client = _clientRepository.GetById(clientDTO.ClientId);

            client.Address = clientDTO.Address;
            client.DateOfBirth = clientDTO.DateOfBirth;
            client.Email = clientDTO.Email;
            client.FullName = clientDTO.FullName;
            client.Mobile = clientDTO.Mobile;
            client.NationalId = clientDTO.NationalId;
            client.NationalityId = clientDTO.NationalityId;
            client.PassportNumber = clientDTO.PassportNumber;
            client.RegisterDate = clientDTO.RegisterDate;

            _clientRepository.Update(client);

            return Ok("Success");
        }

        public IActionResult DeleteClient(int id)
        {
            DomainEntities.DBEntities.Client obj = new DomainEntities.DBEntities.Client();

            obj = _clientRepository.GetById(id);

            _clientRepository.Delete(obj);

            return Ok("Success");
        }

        public IActionResult GetAllClient()
        {
            List <ClientDTO> lstData = new List<ClientDTO>();

            lstData = (from x in _clientRepository.Find(obj => obj.ClientId != -1, obj => obj.Nationality)
                   select new ClientDTO 
                   {
                       ClientId = x.ClientId,
                       Address = x.Address,
                       DateOfBirth = x.DateOfBirth,
                       Email = x.Email,
                       FullName = x.FullName,
                       Mobile = x.Mobile,
                       NationalId = x.NationalId,
                       NationalityId = x.NationalityId,
                       PassportNumber = x.PassportNumber,
                       RegisterDate = x.RegisterDate,
                       NationalityName = x.Nationality.NationalityName,
                   }).ToList ();

            string jsonString = JsonConvert.SerializeObject(lstData, Formatting.None, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            });

            return Ok(jsonString);
        }


        public IActionResult GetClientById(int id) 
        {
            ClientDTO clientDTO = new ClientDTO();
            clientDTO = (from x in _clientRepository.Find(x => x.ClientId == id)
                         select new ClientDTO
                         {
                             ClientId = x.ClientId,
                             Address = x.Address,
                             DateOfBirth = x.DateOfBirth,
                             Email = x.Email,
                             FullName = x.FullName,
                             Mobile = x.Mobile,
                             NationalId = x.NationalId,
                             NationalityId = x.NationalityId,
                             PassportNumber = x.PassportNumber,
                             RegisterDate = x.RegisterDate,
                         }).FirstOrDefault();

            return Ok(clientDTO);
            
        }

        public IActionResult GetAllNationality()
        {
            List<NationalityDTO> lstData = new List<NationalityDTO> ();

            lstData = (from x in _nationalityRepository.GetAll()
                   select new NationalityDTO
                   {
                       NationalityID = x.NationalityId,
                       NationalityName = x.NationalityName,
                   }).ToList();

            string jsonString = JsonConvert.SerializeObject(lstData, Formatting.None, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            });

            return Ok(jsonString);
        }
    }
}
