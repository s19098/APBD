using System;
using System.ComponentModel.DataAnnotations;

namespace Classes5.Models.DTOs.Requests
{
    public class EnrollStudentRequest
    {
        [Required]
        [MaxLength(100)]
        public string IndexNumber { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        public string BirthDate { get; set; }

        [Required]
        [MaxLength(100)]
        public string Studies { get; set; }
    }
}