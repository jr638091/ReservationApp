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

        [Required]
        [DateFormat]
        public string ReservationDate { get; set; }
        public DateTime TargetDate { 
            get { 
                return DateTime.Parse(this.ReservationDate);
            } 
            set {
                ReservationDate = value.ToString("yyyy-MM-ddTHH:mm");
            } }

        [Range(1, 5)]
        public decimal? Rating { get; set; }

        public bool? IsFavorite { get; set; }

        [Required]
        public long? ContactId { get; set; }

        public class DateFormat : ValidationAttribute {
            public string GetErrorMessage () => "Invalid Date Time. Required format yyyy-MM-ddTHH:mm";

            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                DateTime date;
                if(DateTime.TryParseExact(value as String, "yyyy-MM-ddTHH:mm", null, System.Globalization.DateTimeStyles.None, out date)) {
                    return ValidationResult.Success;
                }
                return new ValidationResult(GetErrorMessage());
            }
        }
    }
}