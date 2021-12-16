using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace SimpleMovie.Core.Services.Options
{
    public class UpdateMovieOptions
    {
        public int? MovieId { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public decimal? Rating { get; set; }
        public List<IFormFile> Files { get; set; }
        public UpdateMovieOptions()
        {
            Files = new List<IFormFile>();
        }
    }
}
