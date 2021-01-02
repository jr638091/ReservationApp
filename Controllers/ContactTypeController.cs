using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReservationApp.Data;
using ReservationApp.Models;
using ReservationApp.Dtos;

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
            var items = _repository.ListContactTypes();
            return Ok(_mapper.Map<IEnumerable<ContactTypeReadDto>>(items));
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
    } 
}