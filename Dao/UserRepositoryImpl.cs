using FinanceManagement.Entities;
using FinanceManagement.MyExceptions;
using FinanceManagement.Utility;
using System.Data.SqlClient;
using System.Net.Mail;
namespace FinanceManagement.Dao
{
    public class UserRepositoryImpl : IUserRepository
    {
        
        public bool Register(User user)
        {
            if (!IsValidEmail(user.Email))
            {
                Console.WriteLine("Invalid email format. Please enter a valid email");
                return false;
            }


            // TODO: Logic to insert user into database
            using (SqlConnection sqlConnection = DBConnection.GetConnectionObject())
            using (SqlCommand cmd = new SqlCommand())
           
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "insert into Users(UserName,password,email) values(@UserName,@password,@email)";
                cmd.Parameters.AddWithValue("@UserName", user.Username);
                cmd.Parameters.AddWithValue("@password", user.Password);
                cmd.Parameters.AddWithValue("@email", user.Email);
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                try {
                    if (cmd.ExecuteNonQuery() > 0)
                    
                        return true;
                    }
                    catch (SqlException ex)
                    {
                    // If duplicate email is inserted, show custom message
                    if (ex.Message.Contains("UK_Email_Users"))
                    {
                        Console.WriteLine("This email is already registered. Try logging in");
                    }
                    else
                    {
                        Console.WriteLine($"SQL Error: {ex.Message}");
                    }

                    


                }
            }
            return false;
        }
        private bool IsValidEmail(string email)
        {
            return !string.IsNullOrWhiteSpace(email) &&
                   System.Text.RegularExpressions.Regex.IsMatch(email,
                   @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }



        public User Login(string username, string password)
        {
            using (SqlConnection sqlConnection = DBConnection.GetConnectionObject())
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "select * from Users where UserName = @UserName and Password = @Password";
                cmd.Parameters.AddWithValue("@UserName", username);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Connection = sqlConnection;
                sqlConnection.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new User(
                            (int)reader["user_id"],
                            reader["username"].ToString(),
                            reader["password"].ToString(),
                            reader["email"].ToString()
                        );
                    }
                    else
                    {
                        throw new UserNotFoundException("Invalid username or password");
                    }
   

                }
            }
        }
    }
}


