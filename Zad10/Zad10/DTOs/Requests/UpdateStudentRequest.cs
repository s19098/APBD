using System;
using System.ComponentModel.DataAnnotations;

namespace Zad10.DTOs.Requests
{
    public class UpdateStudentRequest
    {
        [Required(ErrorMessage = "Bad Request")]
        public string IndexNumber { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}