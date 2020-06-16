namespace Kolokwium2.Models
{
    public class MusicianTrack
    {
        public int IdMusicianTrack { get; set; }

        public int IdTrack { get; set; }

        public int IdMusician { get; set; }

        public Track Track { get; set; }
        public Musician Musician { get; set; }
    }
}