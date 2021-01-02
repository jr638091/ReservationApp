using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReservationApp.Models {
    public class Contact{
        [Key]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Display(Name = "Birthday Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime BirthdayDate { get; set; }

        [Required]
        public long ContactTypeId { get; set; }
        public virtual ContactType ContactType { get; set; }

        [Display(Name = "Phone Number")]
        [StringLength(15)]
        [MinLength(10)]
        public string PhoneNumber { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}