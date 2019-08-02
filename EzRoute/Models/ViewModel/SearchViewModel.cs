using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EzRoute.Models.ViewModel
{
    public class SearchViewModel
    {
        [Display(Name="PRODUCTS")]
        public IEnumerable<int> productID { get; set; }
        [Display(Name ="CATEGORY")]
        public int? tagID { get; set; }
        [Display(Name ="AISLE")]
        public int? aisleID { get; set; }
    }


}