using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ReservationApp.Models;
using System;

namespace ReservationApp.Dtos {
    public class ReservationWithContactDto{
        public long Id { get; set; }

        public string Title { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime TargetDate { get; set; }

        public decimal? Rating { get; set; }

        public bool? IsFavorite { get; set; }

        public ContactReadDto Contact { get; set; }
    }

    public class ReservationReadDto {
        public long Id { get; set; }

        public string Title { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime TargetDate { get; set; }

        public decimal? Rating { get; set; }

        public bool? IsFavorite { get; set; }

        public long ContactId { get; set; }
    }

    public class ReservationCreateDto {
        [Required]
        public string Title { get; set; }

        [DataType(DataType.DateTime, ErrorMessage = "Invalid DateFormat")]
        [Required]
        public DateTime? TargetDate { get; set; }

        [Range(1, 5)]
        public decimal? Rating { get; set; }

        public bool? IsFavorite { get; set; }

        [Required]
        public long? ContactId { get; set; }
    }
}