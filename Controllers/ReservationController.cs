using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReservationApp.Data;
using ReservationApp.Models;
using ReservationApp.Dtos;

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

            var ReservationWithContactsDto = _mapper.Map<ReservationWithContactDto>(reservationModel);

            return CreatedAtRoute(nameof(GetReservationById), new { Id = ReservationWithContactsDto.Id }, ReservationWithContactsDto);
        }
    }
}