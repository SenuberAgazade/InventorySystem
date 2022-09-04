using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class ProductCategoryValidator : AbstractValidator<ProductCategory>
    {
        public ProductCategoryValidator()
        {
            RuleFor(c => c.ProductCategoryName).NotEmpty();
            RuleFor(c => c.ProductCategoryName).MinimumLength(2);
        }
    }
}
