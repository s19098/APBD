using System;

namespace PrzykladKolokwium2.Exceptions
{
    public class MusicianDoesNotExistsException : Exception
    {
        public MusicianDoesNotExistsException(string message) : base(message)
        {
        }

        public MusicianDoesNotExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public MusicianDoesNotExistsException()
        {
        }
    }
}
