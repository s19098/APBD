using System;
using System.Data;
using System.Data.SqlClient;
using Zad5.Models;
using Zad5.Models.DTOs.Requests;
using Zad5.Services;
using Microsoft.AspNetCore.Mvc;

namespace Zad5.Controllers
{
    [ApiController]
    [Route("api/enrollments")]
    public class EnrollmentsController : ControllerBase
    {

        private IStudentDbService _service;

        public EnrollmentsController(IStudentDbService service)
        {
            _service = service;
        }
        
        [HttpPost]
        public IActionResult EnrollStudent(EnrollStudentRequest request)
        {
            try
            {
                Enrollment enroll = _service.EnrollStudent(request);
                return Created(enroll.ToString(), enroll);
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
            }
        }

        [Route("promotions")]
        [HttpPost]
        public IActionResult PromoteStudents(PromoteStudentRequest request)
        {
            try
            {
                Enrollment enroll = _service.PromoteStudents(request);
                return Created(enroll.ToString(), enroll);
            }
            catch (Exception exc)
            {
                return NotFound(exc.Message);
            }
        }
    }
}