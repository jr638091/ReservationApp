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
        public ActionResult<IEnumerable<ContactReadDto>> GetContacts([FromQuery] OrderQuery orderQuery, [FromQuery] PaginationQuery paginationQuery)
        {
            string orderAttr;
            bool descending = orderQuery.Descending;
            if (orderQuery.Order != null)
                orderAttr = typeof(Contact).GetProperty(orderQuery.Order) != null ? orderQuery.Order : "Id";
            else orderAttr = "Id";
            IQueryable<Contact> query = _repository.ListContacts();
            query = query.OrderBy(orderAttr);

            if (descending)
            {
                query = query.Reverse();
            }
            int countItems = query.Count();
            // ICollection<Contact> items = query.ToList();
            
            if (paginationQuery.PageIndex > 0)
            {
                var paginated = query.PageResult(paginationQuery.PageIndex, paginationQuery.Count);
                string next = paginationQuery.PageIndex
                              >= paginated.PageCount ? null : $"{HttpContext.Request.Path}?pageIndex={paginated.CurrentPage + 1}&count={paginated.PageSize}";
                string prev = paginationQuery.PageIndex
                              <= 1 ? null : $"{HttpContext.Request.Path}?pageIndex={paginated.CurrentPage - 1}&count={paginationQuery.Count}";
                if(orderQuery.Order != null) {
                    next = next != null ? next + $"&order={orderQuery.Order}&descending={orderQuery.Descending}" : next;
                    prev = prev != null ? prev + $"&order={orderQuery.Order}&descending={orderQuery.Descending}" : prev;
                }
                
                return Ok(new PaginationResult<ContactReadDto>(_mapper.Map<ICollection<ContactReadDto>>(paginated.Queryable.ToList()), countItems, paginated.PageSize, paginated.CurrentPage, next, prev));
            }
            ICollection<Contact> items = query.ToList();
            PaginationResult<ContactReadDto> result = new PaginationResult<ContactReadDto>(_mapper.Map<ICollection<ContactReadDto>>(items), countItems, items.Count, 0, null, null);
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

        [HttpDelete("{id}")]
        public ActionResult DeleteContact(long id)
        {
            if (!_repository.DeleteContact(id)) return NotFound();
            _repository.saveChange();

            return Ok();
        }
    }
}