using System.Collections.Generic;

namespace Kolokwium2.Models
{
    public class Musician
    {
        public int IdMusician { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Nickname { get; set; }

        public ICollection<MusicianTrack> MusicianTracks { get; set; }
    }
}