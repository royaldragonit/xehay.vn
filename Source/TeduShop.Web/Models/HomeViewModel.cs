using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TeduShop.Model.Models;

namespace TeduShop.Web.Models
{
    public class HomeViewModel
    {
        public IEnumerable<SlideViewModel> Slides { set; get; }
        public IEnumerable<Product> LastestProducts { set; get; }
        public IEnumerable<ProductViewModel> TopSaleProducts { set; get; }
        public List<Product> HomeProduct { set; get; }
        public List<ProductCategory> ListCategory { set; get; }
        public List<Product> ProductViewMax { set; get; }
        public List<Product> Videos { set; get; }

        public string Title { set; get; }
        public string MetaKeyword { set; get; }
        public string MetaDescription { set; get; }
    }
}