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
using System.Collections.Generic;
using System.Linq;

namespace Business.Concrete
{
    public class ProductCategoryManager : IProductCategoryService
    {
        IProductCategoryDal _productCategoryDal;

        public ProductCategoryManager(IProductCategoryDal productCategoryDal)
        {
            _productCategoryDal = productCategoryDal;
        }

        [LogAspect(typeof(FileLogger))]
        [ValidationAspect(typeof(ProductCategoryValidator))]
        public IResult Add(ProductCategory productCategory)
        {
            IResult result = BusinessRules.Run(CheckIfProductCategoryNameExists(productCategory.ProductCategoryName));

            if (result != null)
            {
                return result;
            }

            _productCategoryDal.Add(productCategory);

            return new SuccessResult(Messages.ProductCategoryAdded);
        }

        private IResult CheckIfProductCategoryNameExists(string productCategoryName)
        {
            var categoryName = _productCategoryDal.GetAll(p => p.ProductCategoryName == productCategoryName).Any();

            if (categoryName)
            {
                return new ErrorResult(Messages.ProductCategoryNameAlreadyExists);
            }

            return new SuccessResult();
        }

        [LogAspect(typeof(FileLogger))]
        public IResult Delete(ProductCategory productCategory)
        {
            var category = _productCategoryDal.Get(p => p.ProductCategoryId == productCategory.ProductCategoryId);
            _productCategoryDal.Delete(productCategory);
            return new SuccessResult(Messages.ProductCategoryDeleted);
        }

        [LogAspect(typeof(FileLogger))]
        public IDataResult<List<ProductCategory>> GetAll()
        {
            return new SuccessDataResult<List<ProductCategory>>(_productCategoryDal.GetAll(), Messages.ProductListed);
        }

        [LogAspect(typeof(FileLogger))]
        public IDataResult<ProductCategory> GetById(int productCategoryId)
        {
            return new SuccessDataResult<ProductCategory>(_productCategoryDal.Get(p => p.ProductCategoryId == productCategoryId));
        }

        [LogAspect(typeof(FileLogger))]
        [ValidationAspect(typeof(ProductCategoryValidator))]
        public IResult Update(ProductCategory productCategory)
        {
            IResult result = BusinessRules.Run(CheckIfProductCategoryNameExists(productCategory.ProductCategoryName));

            if (result != null)
            {
                return result;
            }

            _productCategoryDal.Update(productCategory);

            return new SuccessResult(Messages.ProductCategoryUpdated);
        }
    }
}
