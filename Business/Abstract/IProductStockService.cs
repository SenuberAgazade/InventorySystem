using Core.Utilities.Results;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IProductStockService
    {
        IDataResult<List<ProductStock>> GetAllStock();
        IDataResult<ProductStock> GetStock(int productId);
        IResult AddStock(int productId, int newAddedProductCount);
        IResult RemoveStock(int productId, int newSoldProductCount);
    }
}
