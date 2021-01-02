using AutoMapper;
using ReservationApp.Dtos;
using ReservationApp.Models;

namespace ReservationApp.Profiles {
    public class ContactTypesProfile : Profile {
        public ContactTypesProfile()
        {
            CreateMap<ContactType, ContactTypeWithContactsDto>();
            CreateMap<ContactType, ContactTypeReadDto>();
            CreateMap<ContactTypeCreateDto, ContactType>();
        }
    }
}