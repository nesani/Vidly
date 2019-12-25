using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class NewMovieViewModal
    {
        public Movie Movie { get; set; }
        public  List<string> Genre { get; set; }
    }
}