using AutoMapper;
using ReservationApp.Dtos;
using ReservationApp.Models;

namespace ReservationApp.Profiles {
    public class ReservationProfile : Profile {
        public ReservationProfile()
        {
            CreateMap<Reservation, ReservationReadDto>();
            CreateMap<Reservation, ReservationWithContactDto>();
            CreateMap<ReservationCreateDto, Reservation>();
        }
    }
}