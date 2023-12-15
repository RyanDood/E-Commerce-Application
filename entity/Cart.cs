namespace Ecommerce_Application.Entities
{
    internal class Cart
    {
        int cartId;
        int customerId;
        int productId;
        int quantity;

        public Cart(int cartID,int customerID,int productID,int quant)
        {
            cartId = cartID;
            customerId = customerID;
            productId = productID;
            quantity = quant;
        }

        public int CartID
        {
            get
            {
                return cartId;
            }
            set
            {
                cartId = value;
            }
        }

        public int CustomerID
        {
            get
            {
                return customerId;
            }
            set
            {
                customerId = value;
            }
        }

        public int ProductId
        {
            get
            {
                return productId;
            }
            set
            {
                productId = value;
            }
        }

        public int Quantity
        {
            get
            {
                return quantity;
            }
            set
            {
                quantity = value;
            }
        }

        public override string ToString()
        {
            return $"Cart ID::{CartID}\t CustomerID::{CustomerID}\t ProductID::{ProductId}\t Quantity::{Quantity}";
        }
    }
}
