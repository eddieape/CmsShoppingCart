using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CmsShoppingCart.Models
{
    public class Page
    {
        public int Id { get; set; }
        [Required, MinLength(2, ErrorMessage = "Minimum length is 2")]
        //[Display(Name = "Fruit")]
        public string Title { get; set; }

        public string Slug { get; set; }
        [Required, MinLength(2, ErrorMessage = "Minimum length is 4")]
        public string Cotent { get; set; }

        public int Sorting { get; set; }

    }
}
