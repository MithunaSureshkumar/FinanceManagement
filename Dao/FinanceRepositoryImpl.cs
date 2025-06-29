using FinanceManagement.Entities;
using FinanceManagement.MyExceptions;
using FinanceManagement.Utility;
using System.Data.SqlClient;

namespace FinanceManagement.Dao
{
    public class FinanceRepositoryImpl : IFinanceRepository
    {
        public bool CreateExpense(Expense expense)
        {
            // TODO: Logic to insert expense
            using (SqlConnection sqlConnection = DBConnection.GetConnectionObject())
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "insert into Expenses (user_id, amount, category_id, date, description) " +
                                  "values (@userId, @amount, @categoryId, @date, @description)";
                cmd.Parameters.AddWithValue("@userId", expense.UserId);
                cmd.Parameters.AddWithValue("@amount", expense.Amount);
                cmd.Parameters.AddWithValue("@categoryId", expense.CategoryId);
                cmd.Parameters.AddWithValue("@date", expense.Date);
                cmd.Parameters.AddWithValue("@description", expense.Description);
                cmd.Connection = sqlConnection;

                sqlConnection.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }


        public bool DeleteExpense(int expenseId)
        {
            //  Logic to delete expense
            using (SqlConnection sqlConnection = DBConnection.GetConnectionObject())
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "delete from Expenses where expense_id = @expenseId";
                cmd.Parameters.AddWithValue("@expenseId", expenseId);
                cmd.Connection = sqlConnection;

                sqlConnection.Open();
                int rows = cmd.ExecuteNonQuery();

                if (rows == 0)
                    throw new ExpenseNotFoundException("Expense ID not found.");
                return true;
            }
        }
        public bool DeleteUser(int userId)
        {
            //  Logic to delete user
            using (SqlConnection sqlConnection = DBConnection.GetConnectionObject())
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "delete from Users where user_id = @userId";
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Connection = sqlConnection;

                sqlConnection.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public List<Expense> GetAllExpenses(int userId)
        {
            // Logic to retrieve expenses for user
            List<Expense> expenseList = new List<Expense>();
            using (SqlConnection sqlConnection = DBConnection.GetConnectionObject())
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "select * from Expenses where user_id = @userId";
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Connection = sqlConnection;

                sqlConnection.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Expense expense = new Expense
                        {
                            ExpenseId = (int)reader["expense_id"],
                            UserId = (int)reader["user_id"],
                            Amount = (decimal)reader["amount"],
                            CategoryId = (int)reader["category_id"],
                            Date = Convert.ToDateTime(reader["date"]),
                            Description = reader["description"].ToString()
                        };
                        expenseList.Add(expense);
                    }
                }
            }
            return expenseList;  // return the list
        }

        public bool UpdateExpense(int userId, Expense expense)
        {
            // Logic to update expense
            using (SqlConnection sqlConnection = DBConnection.GetConnectionObject())
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Parameters.Clear();
                List<string> updates = new List<string>(); //create a new empty list that  hold multiple strings
                cmd.Connection = sqlConnection;

                // Check which field is set -update only one
                // checks whether user enter a new value if it is not zero then it assumes as user need to update it
                if (expense.Amount != 0)
                {
                    updates.Add("amount = @amount");
                    cmd.Parameters.AddWithValue("@amount", expense.Amount);
                }
                else if (expense.CategoryId != 0)
                {
                    updates.Add("category_id = @categoryId");
                    cmd.Parameters.AddWithValue("@categoryId", expense.CategoryId);
                }
                else if (!string.IsNullOrEmpty(expense.Description))
                {
                    updates.Add("description = @description");
                    cmd.Parameters.AddWithValue("@description", expense.Description);
                }
                else if (expense.Date != default(DateTime)) // default date=01/01/0001
                {
                    updates.Add("date = @date");
                    cmd.Parameters.AddWithValue("@date", expense.Date);
                }

                if (updates.Count == 0)
                {
                    Console.WriteLine("Nothing to update.");
                    return false;
                }

                // Build the final query 
                string updated = string.Join(", ", updates); //Joins all items in the updates list into a single string,separated by','
                cmd.CommandText = $"UPDATE Expenses set {updated} where expense_id = @expenseId and user_id = @userId";
                // common parameters to all 
                cmd.Parameters.AddWithValue("@expenseId", expense.ExpenseId);
                cmd.Parameters.AddWithValue("@userId", userId);

                sqlConnection.Open();
                return cmd.ExecuteNonQuery() >0;
              
            }
        }
              //method that returns a list of Category objects
        public List<ExpenseCategory> GetAllCategories()
        {
            List<ExpenseCategory> categories = new List<ExpenseCategory>();

            using (SqlConnection conn = DBConnection.GetConnectionObject())
            {
                
                SqlCommand cmd = new SqlCommand("select * from Expenses_Categories", conn);
                conn.Open();
                cmd.Parameters.Clear();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    categories.Add(new ExpenseCategory  // add ExpenseCategory object to list
                    {
                        CategoryId = (int)reader["category_id"],
                        CategoryName = reader["category_name"].ToString()
                    });
                }
            }

            return categories;
        }
    }
}




