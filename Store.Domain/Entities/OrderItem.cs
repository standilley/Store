using Flunt.Validations;
using System.Diagnostics.Contracts;

namespace Store.Domain.Entities
{
    public class OrderItem : Entity
    {
        public OrderItem(Product product, int quantity)
        {
            AddNotifications(new Contract<OrderItem>()
                .Requires()
                .IsNotNull(product, "Product", "Produto inválido")
                .IsGreaterThan(quantity, 0, "Quantity", "A Quantidade deve ser maior que zero")
                );

            Product = product;                           // verificação:
            Price = product != null ? product.Price : 0; // (if)product != de null, se sim
            Quantity = quantity;                         // add product.price ou:(else)
        }                                                // add valor :0 se for null.
        public Product Product { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }
        public decimal Total() // regra de négocio
        {
            return Price * Quantity;
        }
    }
}
