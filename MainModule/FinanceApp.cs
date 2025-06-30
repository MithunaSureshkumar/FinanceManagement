using FinanceManagement.Dao;
using FinanceManagement.Entities;
using FinanceManagement.MyExceptions;

namespace FinanceManagement.MainModule
{
    internal class FinanceApp
    {
        public void Run() {

            IUserRepository userRepo = new UserRepositoryImpl();
            IFinanceRepository financeRepo = new FinanceRepositoryImpl();

            User? currentUser = null;

            // Login/Register loop
            while (currentUser == null)
            {
                Console.WriteLine("------ Welcome to Finance Management ------");
                Console.WriteLine("1. Register");
                Console.WriteLine("2. Login");
                Console.WriteLine("3. Exit");
                Console.Write("Choose an option: ");
                int option = Convert.ToInt32(Console.ReadLine());
                try
                {
                    switch (option)

                    {
                        case 1:
                            Console.Write("Enter Username: ");
                            string ?regUser = Console.ReadLine();
                            Console.Write("Enter Password: ");
                            string ?regPass = Console.ReadLine();
                            Console.Write("Enter Email: ");
                            string ?email = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(email))
                            {
                                Console.WriteLine("Email is required.");
                                break;
                            }

                            User newUser = new User(0, regUser, regPass, email);
                            bool registered = userRepo.Register(newUser);
                            Console.WriteLine(registered ? " Registration successful!" : "Registration failed!");
                            break;

                        case 2:
                            Console.Write("Enter Username: ");
                            string ?loginUser = Console.ReadLine();
                            Console.Write("Enter Password: ");
                            string ?loginPass = Console.ReadLine();

                            currentUser = userRepo.Login(loginUser, loginPass);
                            if (currentUser != null)
                                Console.WriteLine($"\nLogin successful! Welcome {currentUser.Username}.\n");
                            else
                                Console.WriteLine("Invalid credentials. Try again\n");
                            break;

                        case 3:
                            Console.WriteLine("Exiting application");
                            return;

                        default:
                            Console.WriteLine("Invalid option please choose option 1,2 or 3");
                            break;
                    }
                }

                catch (UserNotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error:{ex.Message}");
                }
            }
                int choice = 0;

            do
            {
               
                Console.WriteLine("1. Add Expense");
                Console.WriteLine("2. Delete User");
                Console.WriteLine("3. Delete Expense");
                Console.WriteLine("4. GetAllExpenses");
                Console.WriteLine("5. Update Expense");
                Console.WriteLine("6. Exit");

                Console.Write("Enter your choice (1-6): ");

                choice = Convert.ToInt32(Console.ReadLine());
                try
                {
                    switch (choice)
                    {

                        case 1:
                            Console.WriteLine("Add Expense selected");
                            // Call method to add expense
                            Console.Write("Enter amount: ");
                            decimal amount = Convert.ToDecimal(Console.ReadLine());

                            List<ExpenseCategory> categories = financeRepo.GetAllCategories();
                            Console.WriteLine("Available Categories:");
                            foreach (var category in categories)
                            {
                                Console.WriteLine($"{category.CategoryId}. {category.CategoryName}");
                            }

                            Console.Write("Enter category ID: ");
                            int categoryId = Convert.ToInt32(Console.ReadLine());

                            Console.Write("Enter description: ");
                            string description = Console.ReadLine();

                            Expense newExpense = new Expense(0, currentUser.UserId, amount, categoryId, DateTime.Now, description);
                            bool added = financeRepo.CreateExpense(newExpense);
                            Console.WriteLine(added ? "Expense added!" : " Failed to add");
                            break;

                        case 2:
                            Console.WriteLine("Delete User selected");
                            // Call method to delete user
                            Console.Write("Are you sure you want to delete your account? (yes/no): ");
                            string confirm = Console.ReadLine();
                            if (confirm ?.ToLower() == "yes")
                            {
                                bool userDeleted = financeRepo.DeleteUser(currentUser.UserId);
                                Console.WriteLine(userDeleted ? "User deleted." : "Could not delete user");
                                return; // Exit after deleting 
                            }
                            break;

                        case 3:
                            Console.WriteLine("Delete Expense selected");
                            // Call method to delete expense
                            Console.Write("Enter Expense ID to delete: ");
                            int delExpId = Convert.ToInt32(Console.ReadLine());

                            bool deleted = financeRepo.DeleteExpense(delExpId); //  only 1 parameter
                            Console.WriteLine(deleted ? $"Expense with id {delExpId} deleted" : $"Expense for id {delExpId} is not found ");
                            break;

                        case 4:
                            Console.WriteLine("Get All Expenses selected");
                            // Call method to GetAllExpenses
                            List<Expense> expenses = financeRepo.GetAllExpenses(currentUser.UserId);
                            if (expenses.Count == 0)
                            {
                                Console.WriteLine("No expenses found.");
                            }
                            else
                            {
                                Console.WriteLine("Your Expenses:");
                                foreach (Expense exp in expenses)
                                {
                                    Console.WriteLine($"ID: {exp.ExpenseId}, Amount: {exp.Amount}, Category: {exp.CategoryId},Date: {exp.Date.ToString("dd-MM-yyyy")}, Description: {exp.Description}");
                                }
                            }
                            break;

                        case 5:
                            Console.WriteLine("Update Expense selected");

                            // 1. Show the user's expenses first
                            List<Expense> expenses1 = financeRepo.GetAllExpenses(currentUser.UserId);
                            if (expenses1.Count == 0)
                            {
                                Console.WriteLine("No expenses found to update.");
                                break;
                            }
                //Prints a list of the user’s expenses to easily know the expense id since one user can have many expenses
                            Console.WriteLine("Your Expenses:");
                            foreach (Expense exp in expenses1)
                            {
                                Console.WriteLine($"ID: {exp.ExpenseId}, Amount: {exp.Amount}, Description: {exp.Description}");
                            }

                            //  2. Ask which one to update
                            Console.Write("Enter Expense ID to update: ");
                            int expId = Convert.ToInt32(Console.ReadLine());

                            // 3. Build the update object
                            Expense expenseToUpdate = new Expense();//creates object that hols updated values
                            expenseToUpdate.ExpenseId = expId;//Set the ExpenseId to the selected ID getting from the user

                            Console.Write("Do you want to update amount? (yes/no): ");
                            if (Console.ReadLine().Trim().ToLower() == "yes")
                            {
                                Console.Write("Enter new amount: ");
                                expenseToUpdate.Amount = Convert.ToDecimal(Console.ReadLine());
                            }

                            Console.Write("Do you want to update category? (yes/no): ");
                            if (Console.ReadLine().Trim().ToLower() == "yes")
                            {
                                Console.Write("Enter new category ID: ");
                                expenseToUpdate.CategoryId = Convert.ToInt32(Console.ReadLine());
                            }

                            Console.Write("Do you want to update description? (yes/no): ");
                            if (Console.ReadLine().Trim().ToLower() == "yes")
                            {
                                Console.Write("Enter new description: ");
                                expenseToUpdate.Description = Console.ReadLine();
                            }

                            Console.Write("Do you want to update date? (yes/no): ");
                            if (Console.ReadLine().Trim().ToLower() == "yes")
                            {
                                Console.Write("Enter new date (yyyy-MM-dd): ");
                                expenseToUpdate.Date = DateTime.Parse(Console.ReadLine());
                            }

                            // 4. Call the update method
                            bool updated = financeRepo.UpdateExpense(currentUser.UserId, expenseToUpdate);//call repository method to perform the update
                            Console.WriteLine(updated ? "Updated successfully!" : "Update failed");
                            break;
                    }
                }
                catch (ExpenseNotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Something went wrong:  {ex.Message}");
                }
            }

            while (choice != 6);
                }
            }
        }
    

        
    



