namespace FinanceManagement.MyExceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException() : base("User not found.") { } // this the default error message

        public UserNotFoundException(string message) : base(message) { } // custom exception
    }
}
