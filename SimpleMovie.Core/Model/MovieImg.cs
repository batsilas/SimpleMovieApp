namespace SimpleMovie.Core.Model
{
    public class MovieImg
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public byte[] ByteArray { get; set; }
    }
}
