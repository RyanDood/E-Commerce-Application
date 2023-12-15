namespace Ecommerce_Application.Entities
{
    internal class Order_Items
    {
        int orderItemId;
        int orderId;
        int productId;
        int quantity;

        public Order_Items(int orderItemID,int orderID, int productID, int quant)
        {
            orderItemId = orderItemID;
            orderId = orderID;
            productId = productID;
            quantity = quant;
        }

        public int OrderItemID
        {
            get
            {
                return orderItemId;
            }
            set
            {
                orderItemId = value;
            }
        }

        public int OrderID
        {
            get
            {
                return orderId;
            }
            set
            {
                orderId = value;
            }
        }

        public int ProductID
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

        public string ToString()
        {
            return $"Order_Item ID::{OrderItemID}\t OrderID::{OrderID}\t ProductID::{ProductID}\t Quantity::{Quantity}";
        }
    }
}
