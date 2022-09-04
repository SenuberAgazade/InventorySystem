using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dto;
using System.Collections.Generic;
using System.Linq;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }
        [LogAspect(typeof(FileLogger))]
        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product)
        {
            IResult result = BusinessRules.Run(CheckIfProductNameExists(product.ProductName, product.ProductCategoryId));

            if (result != null)
            {
                return result;
            }

            _productDal.Add(product);

            return new SuccessResult(Messages.ProductAdded);
        }

        private IResult CheckIfProductNameExists(string productName, int productCategoryId)
        {
            var productNameInCategory = _productDal.GetAll(p => p.ProductName == productName && p.ProductCategoryId == productCategoryId).Any();

            if (productNameInCategory)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }

            return new SuccessResult();
        }

        [LogAspect(typeof(FileLogger))]
        public IDataResult<List<Product>> GetAll()
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductListed);
        }

        [LogAspect(typeof(FileLogger))]
        public IDataResult<List<Product>> GetAllByCategoryId(int categoryId)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.ProductCategoryId == categoryId));
        }

        [LogAspect(typeof(FileLogger))]
        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }

        [LogAspect(typeof(FileLogger))]
        public IDataResult<List<ProductDto>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDto>>(_productDal.GetProductDetails());
        }

        [LogAspect(typeof(FileLogger))]
        [ValidationAspect(typeof(ProductValidator))]
        public IResult Update(Product product)
        {
            IResult result = BusinessRules.Run(CheckIfProductNameExists(product.ProductName, product.ProductCategoryId));

            if (result != null)
            {
                return result;
            }

            _productDal.Update(product);

            return new SuccessResult(Messages.ProductUpdated);
        }

        [LogAspect(typeof(FileLogger))]
        public IResult Delete(Product product)
        {
            var productInfo = _productDal.Get(p => p.ProductId == product.ProductId);
            productInfo.IsDeleted = true;

            _productDal.Update(productInfo);

            return new SuccessResult(Messages.ProductDeleted);
        }
    }
}
