using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ReservationApp.Models;

namespace ReservationApp.Dtos {
    public class ContactWithReservationsDto {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthdayDate { get; set; }
        public ContactTypeReadDto ContactType { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<ReservationReadDto> Reservations { get; set; }
    }

    public class ContactReadDto {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthdayDate { get; set; }
        public ContactTypeReadDto ContactType { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class ContactCreateDto {
        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime? BirthdayDate { get; set; }

        [Required]
        public long? ContactTypeId { get; set; }
        
        [StringLength(15)]
        [MinLength(10)]
        public string PhoneNumber { get; set; }
    }
}