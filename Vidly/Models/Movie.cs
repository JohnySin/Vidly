using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public Genre Genre { get; set; }

        [Display (Name = "Genre")]
        public byte GenreId { get; set; }

        public DateTime DateAdded { get; set; }

        [Required]
        [Display (Name = "Date Released")]
        public DateTime DateReleased { get; set; }

        [Required]
        [Display (Name = "Number In Stock")]
        [Range(1, 20, ErrorMessage = "The field {0} must be between {1} and {2}.")]
        public byte NumberInStock { get; set; }
    }
}