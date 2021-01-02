using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ReservationApp.Models;

namespace ReservationApp.Dtos {
    public class ContactTypeReadDto {
        public long Id { get; set; }
        public string Name { get; set; }
    }

    public class ContactTypeWithContactsDto {
        public long Id { get; set;}
        public string Name { get; set; }
        public ICollection<ContactReadDto> Contacts { get; set; }
    }

    public class ContactTypeCreateDto {
        [Required]
        public string Name {get; set;}
    }
}