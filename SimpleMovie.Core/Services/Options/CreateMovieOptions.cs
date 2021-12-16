using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimpleMovie.Core.Services.Options
{
    public class CreateMovieOptions
    {
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public decimal? Rating { get; set; }
        public List<IFormFile> Files { get; set; }
        public CreateMovieOptions()
        {
            Files = new List<IFormFile>();
        }
    }
}