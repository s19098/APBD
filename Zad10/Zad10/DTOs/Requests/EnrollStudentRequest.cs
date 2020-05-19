using System;
using System.ComponentModel.DataAnnotations;

namespace Zad10.DTOs.Requests
{
    public class EnrollStudentRequest
    {
        [Required(ErrorMessage = "Bad Request")]
        public string IndexNumber { get; set; }

        [Required(ErrorMessage = "Bad Request")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Bad Request")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Bad Request")]
        public string BirthDate { get; set; }

        [Required(ErrorMessage = "Bad Request")]
        public string Studies { get; set; }
    }
}