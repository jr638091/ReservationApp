using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReservationApp.Data;
using ReservationApp.Models;
using ReservationApp.Dtos;
using Microsoft.AspNetCore.JsonPatch;

namespace ReservationApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationAppRepo _repository;
        private readonly IMapper _mapper;

        public ReservationController(IReservationAppRepo repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ReservationReadDto>> GetReservations()
        {
            var items = _repository.ListReservations();
            return Ok(_mapper.Map<IEnumerable<ReservationReadDto>>(items));
        }

        [HttpGet("{id}", Name = "GetReservationById")]
        public ActionResult<ContactReadDto> GetReservationById(long id)
        {
            var reservation = _repository.ReadReservation(id);
            return reservation == null ? NotFound() : Ok(_mapper.Map<ReservationWithContactDto>(reservation));
        }

        [HttpPost]
        public ActionResult<ReservationWithContactDto> PostReservation(ReservationCreateDto reservation)
        {
            var reservationModel = _mapper.Map<Reservation>(reservation);
            _repository.CreateReservation(reservationModel);
            _repository.saveChange();

            var ReservationWithContactsDto = _mapper.Map<ReservationWithContactDto>(_repository.ReadReservation(reservationModel.Id));

            return CreatedAtRoute(nameof(GetReservationById), new { Id = ReservationWithContactsDto.Id }, ReservationWithContactsDto);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateReservation(long id, ReservationCreateDto reservation) {
            var reservationModel = _repository.ReadReservation(id);
            if (reservationModel == null) {
                return NotFound();
            }
            _mapper.Map(reservation, reservationModel);
            _repository.UpdateReservation(reservationModel);
            _repository.saveChange();

            return NoContent();

        }

        [HttpPatch("{id}")]
        public ActionResult PartialUpdateReservation(long id, JsonPatchDocument<ReservationCreateDto> patchDoc) {
            var reservationModel = _repository.ReadReservation(id);
            if (reservationModel == null) {
                return NotFound();
            }

            var reservationToPatch = _mapper.Map<ReservationCreateDto>(reservationModel);
            patchDoc.ApplyTo(reservationToPatch, ModelState);

            if(!TryValidateModel(reservationToPatch)) {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(reservationToPatch, reservationModel);
            _repository.UpdateReservation(reservationModel);
            _repository.saveChange();

            return NoContent();
        }
    }
}