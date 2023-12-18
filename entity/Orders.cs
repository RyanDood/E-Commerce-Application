namespace Ecommerce_Application.Entities
{
    public class Orders
    {
        int orderId;
        int customerId;
        string orderDate;
        int totalPrice;
        string shippingAddress;

        public Orders(int orderID,int customerID,string date,int price,string address)
        {
            orderId = orderID;
            customerId = customerID;
            orderDate = date;
            totalPrice = price;
            shippingAddress = address;
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

        public string OrderDate
        {
            get
            {
                return orderDate;
            }
            set
            {
                orderDate = value;
            }
        }

        public int TotalPrice
        {
            get
            {
                return totalPrice;
            }
            set
            {
                 totalPrice = value;
            }
        }

        public string ShippingAddress
        {
            get
            {
                return shippingAddress;
            }
            set
            {
                shippingAddress = value;
            }
        }

        public override string ToString()
        {
            return $"Order ID::{OrderID}\t CustomerID::{CustomerID}\t OrderDate::{OrderDate}\t TotalPrice::{TotalPrice}\t ShippingAddress::{ShippingAddress}";
        }
    }
}
