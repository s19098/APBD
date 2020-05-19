using System;
using System.ComponentModel.DataAnnotations;

namespace Zad10.DTOs.Requests
{
    public class PromoteStudentRequest
    {
        [Required(ErrorMessage = "Bad Request")]
        public string Studies { get; set; }

        [Required(ErrorMessage = "Bad Request")]
        public int Semester { get; set; }
    }
}