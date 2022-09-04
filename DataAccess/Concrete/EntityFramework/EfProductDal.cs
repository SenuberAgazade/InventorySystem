using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Entities.Dto;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : EfEntityRepositoryBase<Product, InventoryContext>, IProductDal
    {
        public List<ProductDto> GetProductDetails()
        {
            using (InventoryContext context = new InventoryContext())
            {
                var result = from p in context.Products
                             join c in context.ProductCategories on p.ProductCategoryId equals c.ProductCategoryId
                             select new ProductDto
                             {
                                 ProductId = p.ProductId,
                                 ProductName = p.ProductName,
                                 ProductCategoryName = c.ProductCategoryName,
                                 Price = p.Price,
                                 CreatedDate = p.CreatedDate,
                                 State = p.State,
                                 IsDeleted = p.IsDeleted
                             };
                return result.ToList();
            }
        }
    }
}
