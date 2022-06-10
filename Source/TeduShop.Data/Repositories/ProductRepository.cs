using System;
using System.Collections;
using System.Collections.Generic;
using TeduShop.Data.Infrastructure;
using TeduShop.Model.Models;
using System.Data.Entity;
using System.Linq;

namespace TeduShop.Data.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> GetListProductByTag(string tagId, int page, int pageSize, out int totalRow);
        List<Product> GetHomeProduct();
        List<Product> GetProductViewMax();
        List<Product> GetLastest();
        bool CheckUrlIsValid(string categoryAlias);
        List<Product> GetNewsByCategory(string categoryAlias);
        bool CheckUrlIsValid(string categoryAlias, string newsAlias);
        Product GetByAlias(string newsAlias);
        List<Product> GetNewsOtherByAlias(string newsAlias);
    }

    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public bool CheckUrlIsValid(string categoryAlias)
        {
            bool isValidUrl = DbContext.ProductCategories
                                     .Any(x => x.Alias == categoryAlias);
            return isValidUrl;
        }

        public bool CheckUrlIsValid(string categoryAlias, string newsAlias)
        {
            bool isValidUrl = DbContext.Products
                                     .Include(x=>x.ProductCategory)
                                     .Any(x => x.Alias == newsAlias && x.ProductCategory.Alias== categoryAlias);
            return isValidUrl;
        }

        public Product GetByAlias(string newsAlias)
        {
            return DbContext.Products.Include(x => x.ProductCategory).FirstOrDefault(x=>x.Alias==newsAlias);
        }

        public List<Product> GetHomeProduct()
        {
            var data = DbContext.Products
                                        .Include(x => x.ProductCategory)
                                        .Take(7).ToList();
            return data;
        }

        public List<Product> GetLastest()
        {
            var data = DbContext.Products
                                        .Where(x => x.Status)
                                        .OrderByDescending(x => x.CreatedDate)
                                        .Include(x => x.ProductCategory)
                                        .Take(12).ToList();
            return data;
        }

        public IEnumerable<Product> GetListProductByTag(string tagId, int page, int pageSize, out int totalRow)
        {
            var query = from p in DbContext.Products
                        join pt in DbContext.ProductTags
                        on p.ID equals pt.ProductID
                        where pt.TagID == tagId
                        select p;
            totalRow = query.Count();

            return query.OrderByDescending(x => x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize);
        }

        public List<Product> GetNewsByCategory(string categoryAlias)
        {
            var data = DbContext.Products
                                        .Include(x => x.ProductCategory)
                                        .Where(x=>x.ProductCategory.Alias==categoryAlias)
                                        .Take(7).ToList();
            return data;
        }

        public List<Product> GetNewsOtherByAlias(string newsAlias)
        {
            return DbContext.Products.Include(x => x.ProductCategory).Where(x => x.Alias != newsAlias).Take(8).ToList();
        }

        public List<Product> GetProductViewMax()
        {
            var data = DbContext.Products.OrderByDescending(x => x.ViewCount)
                                         .Include(x => x.ProductCategory)
                                         .Take(12).ToList();
            return data;
        }
    }
}