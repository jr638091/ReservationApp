using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReservationApp.Data;
using ReservationApp.Models;
using ReservationApp.Dtos;
using Microsoft.AspNetCore.JsonPatch;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace ReservationApp.Controllers {
    [ApiController]
    [Route("contact-type")]
    public class ContactTypeController: ControllerBase {
        private readonly IReservationAppRepo _repository;
        private readonly IMapper _mapper;

        public ContactTypeController(IReservationAppRepo repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ContactTypeReadDto>> GetContactTypes () {
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
                orderAttr = typeof(ContactType).GetProperty(orderAttr) != null ? queryParams["order"] : "Id";
            }
            IQueryable<ContactType> query = _repository.ListContactTypes();
            query = query.OrderBy(orderAttr);

            if (descending)
            {
                query = query.Reverse();
            }
            int countItems = query.Count();
            // ICollection<ContactType> items = query.ToList();
            if (count > 0)
            {
                if (initial > countItems)
                {
                    return Ok(new PaginationResult<ContactTypeReadDto>(new List<ContactTypeReadDto>(), countItems, 0, initial));
                }
                else
                {
                    query = query.Skip(initial - 1).Take(count);
                }
            }
            ICollection<ContactType> items = query.ToList();
            PaginationResult<ContactTypeReadDto> result = new PaginationResult<ContactTypeReadDto>(_mapper.Map<ICollection<ContactTypeReadDto>>(items), countItems, items.Count, 1);
            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetContactTypeById")]
        public ActionResult<ContactReadDto> GetContactTypeById (long id) {
            var contactType = _repository.ReadContactType(id);
            return contactType == null ? NotFound() : Ok(_mapper.Map<ContactTypeWithContactsDto>(contactType));
        }

        [HttpPost]
        public ActionResult<ContactTypeWithContactsDto> PostContactType (ContactTypeCreateDto contactType) {
            var contactTypeModel = _mapper.Map<ContactType>(contactType);
            _repository.CreateContactType(contactTypeModel);
            _repository.saveChange();

            var ContactTypeWithContactsDto = _mapper.Map<ContactTypeWithContactsDto>(contactTypeModel);

            return CreatedAtRoute(nameof(GetContactTypeById), new {Id = ContactTypeWithContactsDto.Id}, ContactTypeWithContactsDto );
        }

        [HttpPut("{id}")]
        public ActionResult UpdateContactType(long id, ContactTypeCreateDto contactType) {
            var contactTypeModel = _repository.ReadContactType(id);
            if (contactTypeModel == null) {
                return NotFound();
            }
            _mapper.Map(contactType, contactTypeModel);
            _repository.UpdateContactType(contactTypeModel);
            _repository.saveChange();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public ActionResult PartialUpdateContactType(long id, JsonPatchDocument<ContactTypeCreateDto> patchDoc) {
            var contactTypeModel = _repository.ReadContactType(id);
            if (contactTypeModel == null) {
                return NotFound();
            }

            var contactTypeToPatch = _mapper.Map<ContactTypeCreateDto>(contactTypeModel);
            patchDoc.ApplyTo(contactTypeToPatch, ModelState);

            if(!TryValidateModel(contactTypeToPatch)) {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(contactTypeToPatch, contactTypeModel);
            _repository.UpdateContactType(contactTypeModel);
            _repository.saveChange();

            return NoContent();
        }
    } 
}