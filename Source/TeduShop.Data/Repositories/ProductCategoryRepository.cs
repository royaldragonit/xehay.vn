using System.Collections.Generic;
using System.Linq;
using TeduShop.Data.Infrastructure;
using TeduShop.Model.Models;
using System.Data.Entity;
namespace TeduShop.Data.Repositories
{
    public interface IProductCategoryRepository : IRepository<ProductCategory>
    {
        IEnumerable<ProductCategory> GetByAlias(string alias);
        List<ProductCategory> GetListCategory();
    }

    public class ProductCategoryRepository : RepositoryBase<ProductCategory>, IProductCategoryRepository
    {
        public ProductCategoryRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }

        public IEnumerable<ProductCategory> GetByAlias(string alias)
        {
            return this.DbContext.ProductCategories.Where(x => x.Alias == alias);
        }

        public List<ProductCategory> GetListCategory()
        {
            var data= this.DbContext.ProductCategories.Where(x=>x.ID!=2).Include(x=>x.Products)
                                                    .ToList();
            return data;
        }
    }
}