using Core.Utilities.Results;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IProductCategoryService
    {
        IDataResult<List<ProductCategory>> GetAll();
        IDataResult<ProductCategory> GetById(int productCategoryId);
        IResult Add(ProductCategory productCategory);
        IResult Update(ProductCategory productCategory);
        IResult Delete(ProductCategory productCategory);
    }
}
