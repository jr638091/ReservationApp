using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReservationApp.Models {
    public class Reservation {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Title { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataType(DataType.DateTime)]
        [Display(Name = "Creation Date")]
        public DateTime CreationDate { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Reservation Date")]
        [Required]
        public DateTime TargetDate { get; set; }

        [Range(1, 5)]
        public decimal? Rating { get; set; }

        [Display(Name = "Is Favorite")]
        public bool? IsFavorite { get; set; }

        [Required]
        public long ContactId { get; set; }

        [Required]
        public Contact Contact { get; set; }
    }
}