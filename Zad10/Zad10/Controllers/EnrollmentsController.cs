using System;
using Zad10.Services;
using Zad10.DTOs.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Zad10.Controllers
{
    [ApiController]
    [Route("api/enrollments")]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IStudentsDbService _dbService;

        public EnrollmentsController(IStudentsDbService _dbService)
        {
            this._dbService = _dbService;
        }

        [HttpPost]
        public IActionResult CreateStudent(EnrollStudentRequest request)
        {
            var result = _dbService.CreateStudent(request);
            if (result != null) return Ok(result);
            return NotFound();
        }

        [HttpPost("promotions")]
        public IActionResult PromoteStudent(PromoteStudentRequest request)
        {
            var result = _dbService.PromoteStudent(request);
            if (result != null)
            {
                ObjectResult res = new ObjectResult(result);
                res.StatusCode = 201;
                return res;
            }
            return NotFound();
        }
    }
}