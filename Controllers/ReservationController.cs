using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReservationApp.Data;
using ReservationApp.Models;
using ReservationApp.Dtos;
using Microsoft.AspNetCore.JsonPatch;
using System.Linq;
using System.Linq.Dynamic.Core;

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
                orderAttr = typeof(Reservation).GetProperty(orderAttr) != null ? queryParams["order"] : "Id";
            }
            IQueryable<Reservation> query = _repository.ListReservations();
            query = query.OrderBy(orderAttr);

            if (descending)
            {
                query = query.Reverse();
            }
            int countItems = query.Count();
            // ICollection<Reservation> items = query.ToList();
            if (count > 0)
            {
                if (initial > countItems)
                {
                    return Ok(new PaginationResult<ReservationReadDto>(new List<ReservationReadDto>(), countItems, 0, initial));
                }
                else
                {
                    query = query.Skip(initial - 1).Take(count);
                }
            }
            ICollection<Reservation> items = query.ToList();
            PaginationResult<ReservationReadDto> result = new PaginationResult<ReservationReadDto>(_mapper.Map<ICollection<ReservationReadDto>>(items), countItems, items.Count, 1);
            return Ok(result);
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