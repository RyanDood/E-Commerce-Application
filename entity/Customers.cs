namespace Ecommerce_Application.Entities
{
    public class Customers
    {
        int customer_id;
        string name;
        string email;
        string password;

        public Customers(int customerID,string customerName,string customerEmail,string customerPassword)
        {
            customer_id = customerID;
            name = customerName;
            email = customerEmail;
            password = customerPassword;
        }

        public int CustomerID {
            get {
                return customer_id;
            } set
            {
                customer_id = value;
            }
        }

        public string CustomerName {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public string CustomerEmail
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
            }
        }

        public string CustomerPassword
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }

        public override string ToString()
        {
            return $"Customer ID::{CustomerID}\t CustomerName::{CustomerName}\t CustomerEmail::{CustomerEmail}\t CustomerPassword::{CustomerPassword}";
        }
    }
}
