using Microsoft.EntityFrameworkCore;
using Kolokwium2.Exceptions;
using Kolokwium2.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium2.Services
{
    public class SqlServerMusicDbService : IMusicDbService
    {

        private readonly MusicDbContext _context;

        public async Task GetMusician(int id)
        {
            var musician = await _context.Musicians
                                 .Include(m => m.MusicianTracks)
                                 .SingleOrDefaultAsync(m => m.IdMusician == id);

            if (musician == null)
            {
                throw new MusicianDoesNotExistsException($"Musician with an id={id} does not exists");
            }

            if (!await _context.Tracks.AnyAsync(t => t.MusicianTracks.Any(mt => mt.IdMusician == id) && t.IdMusicAlbum != null))
            {
                throw new MusicianReleasedAnAlbumException($"Musician with an id={id} released at least a single album");
            }

            _context.MusicianTracks.GetRange(musician.MusicianTracks);
            _context.Musicians.Get(musician);
            _context.SaveChanges();
        }
    }
}
