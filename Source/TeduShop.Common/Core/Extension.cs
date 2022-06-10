using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeduShop.Model.Models;

namespace TeduShop.Common.Core
{
    public static class Extension
    {
        public static string GeneraSlugNews(this Product product)
        {
            if (product == null)
                return string.Empty;
            string slug = string.Concat("/", product.ProductCategory.Alias, "/", product.Alias,".html");
            return slug;
        }
        public static string GeneraSlugCategory(this ProductCategory productCategory)
        {
            if (productCategory == null)
                return string.Empty;
            string slug = string.Concat("/", productCategory.Alias);
            return slug;
        }
    }
}
