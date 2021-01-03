using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReservationApp.Data;
using ReservationApp.Models;
using ReservationApp.Dtos;
using Microsoft.AspNetCore.JsonPatch;

namespace ReservationApp.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class ContactController: ControllerBase {
        private readonly IReservationAppRepo _repository;
        private readonly IMapper _mapper;

        public ContactController(IReservationAppRepo repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper; 
        }

        [HttpGet]
        public ActionResult<IEnumerable<ContactReadDto>> GetContacts () {
            var items = _repository.ListContacts();
            return Ok(_mapper.Map<IEnumerable<ContactReadDto>>(items));
        }

        [HttpGet("{id}", Name = "GetContactById")]
        public ActionResult<ContactWithReservationsDto> GetContactById (long id) {
            Contact contact = _repository.ReadContact(id);
            return contact == null ? NotFound() : Ok(_mapper.Map<ContactWithReservationsDto>(contact));
        }

        [HttpPost]
        public ActionResult<ContactWithReservationsDto> PostContact (ContactCreateDto contact) {
            var contactModel = _mapper.Map<Contact>(contact);
            _repository.CreateContact(contactModel);
            _repository.saveChange();
            ContactWithReservationsDto contactWithReservationsDto = _mapper.Map<ContactWithReservationsDto>(_repository.ReadContact(contactModel.Id));
            return CreatedAtRoute(nameof(GetContactById), new {Id = contactWithReservationsDto.Id}, contactWithReservationsDto);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateContact(long id, ContactCreateDto contact) {
            var contactModel = _repository.ReadContact(id);
            if (contactModel == null) {
                return NotFound();
            }
            _mapper.Map(contact, contactModel);
            _repository.UpdateContact(contactModel);
            _repository.saveChange();

            return NoContent();

        }

        [HttpPatch("{id}")]
        public ActionResult PartialUpdateContact(long id, JsonPatchDocument<ContactCreateDto> patchDoc) {
            var contactModel = _repository.ReadContact(id);
            if (contactModel == null) {
                return NotFound();
            }

            var contactToPatch = _mapper.Map<ContactCreateDto>(contactModel);
            patchDoc.ApplyTo(contactToPatch, ModelState);

            if(!TryValidateModel(contactToPatch)) {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(contactToPatch, contactModel);
            _repository.UpdateContact(contactModel);
            _repository.saveChange();

            return NoContent();
        }
    } 
}