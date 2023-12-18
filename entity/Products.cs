namespace Ecommerce_Application.Entities
{
    public class Products
    {
        int product_id;
        string product_name;
        int price;
        string description;
        int stockQuantity;
        public Products(int productID,string productName,int amount,string desc,int quantity)
        {
            product_id = productID;
            product_name = productName;
            price = amount;
            description = desc;
            stockQuantity = quantity;   
        }

        public int ProductID {
            get { 
                return product_id;
            } 
            set { 
                product_id = value; 
            }
        }

        public string ProductName {
            get {
                return product_name; 
            } 
            set {
                product_name = value;
            }
        }

        public int Price {
            get { 
                return price;
            }
            set
            {
                price = value;
            }
        }
        public string Description { 
            get { 
                return description;
            } 
            set { 
                description = value;
            }
        }

        public int StockQuantity
        {
            get
            {
                return stockQuantity;
            }
            set
            {
                stockQuantity = value;
            }
        }

        public override string ToString()
        {
            return $"Product ID::{ProductID}\t ProductName::{ProductName}\t ProductPrice::{Price}\t Description::{Description}\t StockQuantity::{StockQuantity}";
        }
    }
}
