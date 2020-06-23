using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Kolokwium1Poprawa.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kolokwium1Poprawa.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private IDbService _dbService;
        public TasksController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet("{idMember:int}")]
        public IActionResult GetTeamMember(int idMember)
        {
            try
            {
                return Ok(_dbService.GetTeamMember(idMember));//OK200
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);//404
            }
        }

        [HttpDelete("{idProject:int}")]
        public IActionResult DeleteProject(int idProject)
        {
            try
            {
                return Ok(_dbService.DeleteProject(idProject));//OK200
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);//404
            }
        }
    }
}


