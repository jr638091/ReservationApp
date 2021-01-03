using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReservationApp.Data;
using ReservationApp.Models;
using ReservationApp.Dtos;
using Microsoft.AspNetCore.JsonPatch;
using System.Linq;
using System.Reflection;
using System.Linq.Dynamic.Core;
using System;

namespace ReservationApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly IReservationAppRepo _repository;
        private readonly IMapper _mapper;

        public ContactController(IReservationAppRepo repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ContactReadDto>> GetContacts()
        {
            int initial = 1;
            int count = -1;
            string orderAttr = "Id";
            bool descending = false;
            var queryParams = HttpContext.Request.Query;
            if (queryParams.Keys.Contains("descending"))
            {
                descending = bool.Parse(queryParams["descending"]);
            }
            if (queryParams.Keys.Contains("initial"))
            {
                initial = int.Parse(queryParams["initial"]);
            }
            if (queryParams.Keys.Contains("count"))
            {
                count = int.Parse(queryParams["count"]);
            }
            if (queryParams.Keys.Contains("order"))
            {
                orderAttr = typeof(Contact).GetProperty(orderAttr) != null ? queryParams["order"] : "Id";
            }
            IQueryable<Contact> query = _repository.ListContacts();
            query = query.OrderBy(orderAttr);

            if (descending)
            {
                query = query.Reverse();
            }
            int countItems = query.Count();
            // ICollection<Contact> items = query.ToList();
            if (count > 0)
            {
                if (initial > countItems)
                {
                    return Ok(new PaginationResult<ContactReadDto>(new List<ContactReadDto>(), countItems, 0, initial));
                }
                else
                {
                    query = query.Skip(initial - 1).Take(count);
                }
            }
            ICollection<Contact> items = query.ToList();
            PaginationResult<ContactReadDto> result = new PaginationResult<ContactReadDto>(_mapper.Map<ICollection<ContactReadDto>>(items), countItems, items.Count, 1);
            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetContactById")]
        public ActionResult<ContactWithReservationsDto> GetContactById(long id)
        {
            Contact contact = _repository.ReadContact(id);
            return contact == null ? NotFound() : Ok(_mapper.Map<ContactWithReservationsDto>(contact));
        }

        [HttpPost]
        public ActionResult<ContactWithReservationsDto> PostContact(ContactCreateDto contact)
        {
            var contactModel = _mapper.Map<Contact>(contact);
            _repository.CreateContact(contactModel);
            _repository.saveChange();
            ContactWithReservationsDto contactWithReservationsDto = _mapper.Map<ContactWithReservationsDto>(_repository.ReadContact(contactModel.Id));
            return CreatedAtRoute(nameof(GetContactById), new { Id = contactWithReservationsDto.Id }, contactWithReservationsDto);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateContact(long id, ContactCreateDto contact)
        {
            var contactModel = _repository.ReadContact(id);
            if (contactModel == null)
            {
                return NotFound();
            }
            _mapper.Map(contact, contactModel);
            _repository.UpdateContact(contactModel);
            _repository.saveChange();

            return NoContent();

        }

        [HttpPatch("{id}")]
        public ActionResult PartialUpdateContact(long id, JsonPatchDocument<ContactCreateDto> patchDoc)
        {
            var contactModel = _repository.ReadContact(id);
            if (contactModel == null)
            {
                return NotFound();
            }

            var contactToPatch = _mapper.Map<ContactCreateDto>(contactModel);
            patchDoc.ApplyTo(contactToPatch, ModelState);

            if (!TryValidateModel(contactToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(contactToPatch, contactModel);
            _repository.UpdateContact(contactModel);
            _repository.saveChange();

            return NoContent();
        }
    }
}