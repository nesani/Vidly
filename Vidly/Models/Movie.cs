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

        [Required(ErrorMessage ="Name is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Genre is Required")]
        public string Genre { get; set; }

        [Required (ErrorMessage ="Release Date is Required")]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }

        public DateTime DateAdded { get; set; }

        [Required(ErrorMessage = "Stock amount is Required")]
        [Range(1,20)]
        [Display(Name = "Number in Stock")]
        public short Stock { get; set; }

    }
}