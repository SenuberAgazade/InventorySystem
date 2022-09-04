using System;
using Core.Entities;

namespace Entities.Dto
{
    public class ProductDto : IDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCategoryName { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public string State { get; set; }
        public bool IsDeleted { get; set; }
    }
}
