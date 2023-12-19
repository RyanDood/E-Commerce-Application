namespace E_Commerce_Application.exception
{
    public class OrderNotFoundException:ApplicationException
    {
        public OrderNotFoundException(string msg): base(msg)
        {
            
        }
    }
}
