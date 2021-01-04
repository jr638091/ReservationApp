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
        public ActionResult<IEnumerable<ReservationReadDto>> GetReservations([FromQuery] OrderQuery orderQuery, [FromQuery] PaginationQuery paginationQuery)
        {
            string orderAttr;
            bool descending = orderQuery.Descending;
            if (orderQuery.Order != null)
                orderAttr = typeof(Reservation).GetProperty(orderQuery.Order) != null ? orderQuery.Order : "Id";
            else orderAttr = "Id";
            IQueryable<Reservation> query = _repository.ListReservations();
            query = query.OrderBy(orderAttr);

            if (descending)
            {
                query = query.Reverse();
            }
            int countItems = query.Count();
            // ICollection<Reservation> items = query.ToList();

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

                return Ok(new PaginationResult<ReservationReadDto>(_mapper.Map<ICollection<ReservationReadDto>>(paginated.Queryable.ToList()), countItems, paginated.PageSize, paginated.CurrentPage, next, prev));
            }
            ICollection<Reservation> items = query.ToList();
            PaginationResult<ReservationReadDto> result = new PaginationResult<ReservationReadDto>(_mapper.Map<ICollection<ReservationReadDto>>(items), countItems, items.Count, 0, null, null);
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
        public ActionResult UpdateReservation(long id, ReservationCreateDto reservation)
        {
            var reservationModel = _repository.ReadReservation(id);
            if (reservationModel == null)
            {
                return NotFound();
            }
            _mapper.Map(reservation, reservationModel);
            _repository.UpdateReservation(reservationModel);
            _repository.saveChange();

            return NoContent();

        }

        [HttpPatch("{id}")]
        public ActionResult PartialUpdateReservation(long id, JsonPatchDocument<ReservationCreateDto> patchDoc)
        {
            var reservationModel = _repository.ReadReservation(id);
            if (reservationModel == null)
            {
                return NotFound();
            }

            var reservationToPatch = _mapper.Map<ReservationCreateDto>(reservationModel);
            patchDoc.ApplyTo(reservationToPatch, ModelState);

            if (!TryValidateModel(reservationToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(reservationToPatch, reservationModel);
            _repository.UpdateReservation(reservationModel);
            _repository.saveChange();

            return NoContent();
        }
    }
}