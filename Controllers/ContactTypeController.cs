using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReservationApp.Data;
using ReservationApp.Models;
using ReservationApp.Dtos;
using Microsoft.AspNetCore.JsonPatch;
using System.Linq;
using System.Linq.Dynamic.Core;
using System;

namespace ReservationApp.Controllers
{
    [ApiController]
    [Route("contact-type")]
    public class ContactTypeController : ControllerBase
    {
        private readonly IReservationAppRepo _repository;
        private readonly IMapper _mapper;

        public ContactTypeController(IReservationAppRepo repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ContactTypeReadDto>> GetContactTypes([FromQuery] OrderQuery orderQuery, [FromQuery] PaginationQuery paginationQuery)
        {
            string orderAttr;
            bool descending = orderQuery.Descending;
            if (orderQuery.Order != null)
                orderAttr = typeof(ContactType).GetProperty(orderQuery.Order) != null ? orderQuery.Order : "Id";
            else orderAttr = "Id";
            IQueryable<ContactType> query = _repository.ListContactTypes();
            query = query.OrderBy(orderAttr);

            if (descending)
            {
                query = query.Reverse();
            }
            int countItems = query.Count();
            // ICollection<ContactType> items = query.ToList();

            if (paginationQuery.PageIndex > 0)
            {
                var paginated = query.PageResult(paginationQuery.PageIndex, paginationQuery.Count);
                string next = paginationQuery.PageIndex
                              >= paginated.PageCount ? null : $"{HttpContext.Request.Path}?pageIndex={paginated.CurrentPage + 1}&count={paginated.PageSize}";
                string prev = paginationQuery.PageIndex
                              <= 1 ? null : $"{HttpContext.Request.Path}?pageIndex={paginated.CurrentPage - 1}&count={paginationQuery.Count}";
                if (orderQuery.Order != null)
                {
                    next = next != null ? next + $"&order={orderQuery.Order}&descending={orderQuery.Descending}" : next;
                    prev = prev != null ? prev + $"&order={orderQuery.Order}&descending={orderQuery.Descending}" : prev;
                }

                return Ok(new PaginationResult<ContactTypeReadDto>(_mapper.Map<ICollection<ContactTypeReadDto>>(paginated.Queryable.ToList()), countItems, paginated.PageSize, paginated.CurrentPage, next, prev));
            }
            ICollection<ContactType> items = query.ToList();
            PaginationResult<ContactTypeReadDto> result = new PaginationResult<ContactTypeReadDto>(_mapper.Map<ICollection<ContactTypeReadDto>>(items), countItems, items.Count, 0, null, null);
            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetContactTypeById")]
        public ActionResult<ContactReadDto> GetContactTypeById(long id)
        {
            var contactType = _repository.ReadContactType(id);
            return contactType == null ? NotFound() : Ok(_mapper.Map<ContactTypeWithContactsDto>(contactType));
        }

        [HttpPost]
        public ActionResult<ContactTypeWithContactsDto> PostContactType(ContactTypeCreateDto contactType)
        {
            var contactTypeModel = _mapper.Map<ContactType>(contactType);
            _repository.CreateContactType(contactTypeModel);
            _repository.saveChange();

            var ContactTypeWithContactsDto = _mapper.Map<ContactTypeWithContactsDto>(contactTypeModel);

            return CreatedAtRoute(nameof(GetContactTypeById), new { Id = ContactTypeWithContactsDto.Id }, ContactTypeWithContactsDto);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateContactType(long id, ContactTypeCreateDto contactType)
        {
            var contactTypeModel = _repository.ReadContactType(id);
            if (contactTypeModel == null)
            {
                return NotFound();
            }
            _mapper.Map(contactType, contactTypeModel);
            _repository.UpdateContactType(contactTypeModel);
            _repository.saveChange();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public ActionResult PartialUpdateContactType(long id, JsonPatchDocument<ContactTypeCreateDto> patchDoc)
        {
            var contactTypeModel = _repository.ReadContactType(id);
            if (contactTypeModel == null)
            {
                return NotFound();
            }

            var contactTypeToPatch = _mapper.Map<ContactTypeCreateDto>(contactTypeModel);
            patchDoc.ApplyTo(contactTypeToPatch, ModelState);

            if (!TryValidateModel(contactTypeToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(contactTypeToPatch, contactTypeModel);
            _repository.UpdateContactType(contactTypeModel);
            _repository.saveChange();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteContactType(long id)
        {
            if (!_repository.DeleteContactType(id)) return NotFound();
            _repository.saveChange();
            return Ok();
        }
    }
}