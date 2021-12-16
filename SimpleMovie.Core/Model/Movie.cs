using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimpleMovie.Core.Model
{
    public class Movie
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Title { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; }
        [Required, StringLength(150)]
        public string Description { get; set; }
        [StringLength(50)]
        public string Genre { get; set; }
        public decimal? Rating { get; set; }

        public List<MovieImg> MovieImgs { get; set; }

        public Movie() {
            MovieImgs = new List<MovieImg>();
        }
    }
}
