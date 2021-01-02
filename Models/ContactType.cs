using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReservationApp.Models {
    public class ContactType {
        [Key]
        public long Id {get; set;}
        [Required]
        public string Name { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
    }
}