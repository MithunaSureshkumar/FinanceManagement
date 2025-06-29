namespace FinanceManagement.Entities
{
    public class User
    {
         int userId;
         string username;
         string password;
         string email;

        //default constructor
        public User() { }

        //Parameterized constructors
        public User(int userId, string username, string password, string email)
        {
            this.userId = userId;
            this.username = username;
            this.password = password;
            this.email = email;
        }

        //getters and setters
        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        //To string method
        public override string ToString()
        {
            return $"UserId: {userId}, Username: {username}, Email: {email}";
        }
    }
}
