using Core.Entities;
using System;

namespace Entities.Concrete
{
    public class Product : IEntity
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductCategoryId { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public string State { get; set; }
        public bool IsDeleted { get; set; }
    }
}
