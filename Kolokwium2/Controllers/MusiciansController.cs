using Microsoft.AspNetCore.Mvc;
using Kolokwium2.Exceptions;
using Kolokwium2.Services;
using System.Threading.Tasks;

namespace Kolokwium2.Controllers
{
    [Route("api/musician")]
    [ApiController]
    public class MusiciansController : ControllerBase
    {
        private readonly IMusicDbService _dbService;

        public MusiciansController(IMusicDbService dbService)
        {
            _dbService = dbService;
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetMusician(int id)
        {
            try
            {
                await _dbService.GetMusician(id);
                return NoContent();
            }
            catch (MusicianDoesNotExistsException exc)
            {
                return NotFound(exc.Message);
            }
        }

    }
}
