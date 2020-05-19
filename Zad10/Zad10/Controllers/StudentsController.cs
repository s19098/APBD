using System;
using Zad10.DTOs.Requests;
using Zad10.Services;
using Microsoft.AspNetCore.Mvc;

namespace Zad10.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentsDbService _dbService;

        public StudentsController(IStudentsDbService _dbService)
        {
            this._dbService = _dbService;
        }

        [HttpGet]
        public IActionResult GetStudent()
        {
            return Ok(_dbService.GetStudents());
        }

        [HttpPut]
        public IActionResult UpdateStudent(UpdateStudentRequest request)
        {
            var result = _dbService.UpdateStudent(request);
            if (result.Equals("OK"))
                return Ok("Student was updated");

            return NotFound();
        }

        [HttpDelete("{index}")]
        public IActionResult DeleteStudent(string index)
        {
            var result = _dbService.DeleteStudent(index);
            if (result.Equals("OK"))
                return Ok("Student was deleted");
            else if (result.Equals("Bad Request"))
                return BadRequest();
            else
                return NotFound();
        }
    }
}