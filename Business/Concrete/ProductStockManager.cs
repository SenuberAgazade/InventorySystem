using Business.Abstract;
using Business.Constants;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class ProductStockManager : IProductStockService
    {
        IProductStockDal _productStockDal;
        IProductService _productService;

        public ProductStockManager(IProductStockDal productStockDal, IProductService productService)
        {
            _productStockDal = productStockDal;
            _productService = productService;
        }

        [LogAspect(typeof(FileLogger))]
        public IResult AddStock(int productId, int newAddedProductCount)
        {
            IResult result = BusinessRules.Run(CheckIfProductIdInvalid(productId));

            if (result != null)
            {
                return result;
            }

            var productStock = GetStock(productId).Data;
            productStock.StockCount += newAddedProductCount;
            _productStockDal.Update(productStock);
            return new SuccessResult();
        }

        [LogAspect(typeof(FileLogger))]
        public IDataResult<List<ProductStock>> GetAllStock()
        {
            return new SuccessDataResult<List<ProductStock>>(_productStockDal.GetAll());
        }

        [LogAspect(typeof(FileLogger))]
        public IDataResult<ProductStock> GetStock(int productId)
        {
            return new SuccessDataResult<ProductStock>(_productStockDal.Get(p => p.ProductId == productId));
        }

        [LogAspect(typeof(FileLogger))]
        public IResult RemoveStock(int productId, int newSoldProductCount)
        {
            IResult result = BusinessRules.Run(CheckIfProductStockEqualsToZero(productId), CheckIfProductIdInvalid(productId));

            if (result != null)
            {
                return result;
            }

            var productStock = GetStock(productId).Data;
            productStock.StockCount -= newSoldProductCount;
            _productStockDal.Update(productStock);
            return new SuccessResult();
        }

        private IResult CheckIfProductStockEqualsToZero(int productId)
        {
            var productStock = GetStock(productId).Data;

            if (productStock.StockCount == 0)
            {
                return new ErrorResult(Messages.OutOfStock);
            }

            return new SuccessResult();
        }

        private IResult CheckIfProductIdInvalid(int productId)
        {
            var product = _productService.GetById(productId).Data;

            if (product == null)
            {
                return new ErrorResult(Messages.InvalidProductId);
            }

            return new SuccessResult();
        }
    }
}
