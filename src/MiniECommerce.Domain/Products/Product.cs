using MiniECommerce.Categories;
using Volo.Abp.Domain.Entities.Auditing;

namespace MiniECommerce.Products
{
    public class Product : AuditedEntity<int>
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string DescriptionAr { get; set; }
        public string DescriptionEn { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public Product(int id)
        {
            Id = id;
        }
    }
}
