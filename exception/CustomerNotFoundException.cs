namespace E_Commerce_Application.exception
{
    public class CustomerNotFoundException:ApplicationException
    {
        public CustomerNotFoundException(string msg) : base(msg)
        {
            
        }
    }
}
