using System;
using System.Data;
using System.Data.SqlClient;
using Classes5.Models;
using Classes5.Models.DTOs.Requests;
using Classes5.Services;
using Microsoft.AspNetCore.Mvc;

namespace Classes5.Controllers
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