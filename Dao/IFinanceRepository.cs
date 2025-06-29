using FinanceManagement.Entities;

namespace FinanceManagement.Dao
{
    public interface IFinanceRepository
    {
            

            // 1. Add a new expense
            bool CreateExpense(Expense expense);

            // 2. Delete a user by ID
            bool DeleteUser(int userId);

            // 3. Delete an expense by ID
            bool DeleteExpense(int expenseId);

            // 4. Get all expenses for a specific user
            List<Expense> GetAllExpenses(int userId);

            // 5. Update an existing expense for a user
            bool UpdateExpense(int userId, Expense expense);

                //  creating getall categories method
                List<ExpenseCategory> GetAllCategories();

    }
    }


