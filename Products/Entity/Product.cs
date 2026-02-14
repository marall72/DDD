using Shared.ValueObject;

namespace Products.Entity
{
    public class Product
    {
        public Product()
        {
            
        }

        public Product(Guid id, string title, Price price, string description, int quantity)
        {
            
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public Price Price { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }

        public static Product Create(Guid id, string title, decimal priceAmount, string priceCurrency, string description, int quantity)
        {
            return new Product(id, title, new Price(priceAmount, priceCurrency), description, quantity);
        }
    }
}
