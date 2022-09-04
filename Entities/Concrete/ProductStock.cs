using Core.Entities;

namespace Entities.Concrete
{
    public class ProductStock : IEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int StockCount { get; set; }
    }
}
