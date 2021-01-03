using AutoMapper;
using ReservationApp.Dtos;
using ReservationApp.Models;

namespace ReservationApp.Profiles {
    public class ContactsProfile : Profile {
        public ContactsProfile()
        {
            CreateMap<Contact, ContactReadDto>();
            CreateMap<Contact, ContactWithReservationsDto>();
            CreateMap<ContactCreateDto, Contact>();
            CreateMap<Contact, ContactCreateDto>();
        }
    }
}