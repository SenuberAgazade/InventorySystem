using Core.DataAccess;
using Entities.Concrete;
using Entities.Dto;
using System.Collections.Generic;

namespace DataAccess.Abstract
{
    public interface IProductDal : IEntityRepository<Product>
    {
        List<ProductDto> GetProductDetails();
    }
}
