using Shared.ValueObject;

namespace Products.Entity
{
    public class Product
    {
        public Product()
        {
            
        }

        public Product(Guid id, string title, Price price, string description, string quantity)
        {
            
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public Price Price { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
    }
}
