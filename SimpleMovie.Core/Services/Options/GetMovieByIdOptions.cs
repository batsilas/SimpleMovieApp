using System;

namespace SimpleMovie.Core.Services.Options
{
    public class GetMovieByIdOptions
    {
        public int? MovieId { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public decimal? Rating { get; set; }

    }
}
