namespace E_Commerce_Application.Exception
{
    internal class CustomerNotFoundException:ApplicationException
    {
        public CustomerNotFoundException(string msg) : base(msg)
        {
            
        }
    }
}
