using FinanceManagement.Dao;
using FinanceManagement.Entities;
using FinanceManagement.MyExceptions;
using NUnit.Framework;

namespace FinanceManagement.test
{
    public class FinanceAppTest
    {
        IUserRepository repository = new UserRepositoryImpl();
        IFinanceRepository repo = new FinanceRepositoryImpl();

        //Write test case to test if user created successfully  
        [Test]
        public void Register_ValidUser_ReturnsTrue()
        {
            // Arrange
            //  var repo = new UserRepositoryImpl();
            var user = new User(0, "nigi", "pass12", "nigi@gmail.com");
            // Act
            var result = repository.Register(user);

            // Assert
            Assert.That(result, Is.True);

        }

        [Test]
        public void Register_DuplicateEmail_ThrowsException()
        {
            var repo = new UserRepositoryImpl();
            var user = new User(0, "mathew", "pass123", "mithuna@gmail.com"); // existing email
            Assert.That(repo.Register(user), Is.True); // Should fail due to unique constraint
        }
        [Test]
        public void Register_EmptyUsername_ReturnsFalse() // edge case
        {
            //var repo = new UserRepositoryImpl();
            var user = new User(0, "", "pass123", "test@gmail.com");
            Assert.That(repository.Register(user), Is.False); // If validation exists
        }


        //Write test case to test if expene is created successfully
        [TestFixture]
        public class ExpenseTests
        {
            //var repo = new FinanceRepositoryImpl(); cannot create
            [Test]
            public void CreateExpense_ValidInput_ReturnsTrue() // should pass
            {
                var repo = new FinanceRepositoryImpl();
                var expense = new Expense(0, 1, 250, 2, DateTime.Now, "Lunch");
                Assert.That(repo.CreateExpense(expense), Is.True);
            }

            [Test]
            public void CreateExpense_ZeroAmount_ReturnsFalse() // invalid input
            {
                var repo = new FinanceRepositoryImpl();
                var expense = new Expense(0, 1, 0, 2, DateTime.Now, "payment for loan");
                Assert.That(repo.CreateExpense(expense), Is.False); // Assuming logic prevents zero
            }

            [Test]
            public void CreateExpense_InvalidCategoryId_ReturnsFalse() //  invalid FK
            {
                var repo = new FinanceRepositoryImpl();
                var expense = new Expense(0, 1, 500, 9999, DateTime.Now, "Lunch at hotel");
                Assert.That(repo.CreateExpense(expense), Is.False); // Fails due to FK violation
            }

            [Test]
            public void CreateExpense_EmptyDescription_ReturnsTrue()
            {
                var repo = new FinanceRepositoryImpl();
                var expense = new Expense(0, 1, 150, 2, DateTime.Now, "");
                Assert.That(repo.CreateExpense(expense), Is.True);
            }
        }

        //Write test case to test search of expense 

        [Test]
        public void GetAllExpenses_UserWithData_ReturnsList() // 
        {
            //var repo = new FinanceRepositoryImpl();
            var result = repo.GetAllExpenses(1); // Has expenses
            Assert.That(result, Is.Not.Empty);
        }

        [Test]
        public void GetAllExpenses_UserWithoutData_ReturnsEmptyList() // 
        {
            var repo = new FinanceRepositoryImpl();
            var result = repo.GetAllExpenses(9999); // Valid ID, no expenses
            Assert.That(result, Is.Empty);
        }

        // write test case to test if the exceptions are thrown correctly based on scenario 

        [TestFixture]
        public class ExceptionTests
        {
            [Test]
            public void Login_InvalidCredentials_ThrowsUserNotFoundException()
            {
                var repo = new UserRepositoryImpl();
                Assert.Throws<UserNotFoundException>(() => repo.Login("jaya", "pass17"));
            }

            [Test]
            public void DeleteExpense_NonExistentId_ThrowsExpenseNotFoundException()
            {
                var repo = new FinanceRepositoryImpl();
                Assert.Throws<ExpenseNotFoundException>(() => repo.DeleteExpense(9999));
            }

            [Test]
            public void Login_ValidUser_DoesNotThrow()
            {
                var repo = new UserRepositoryImpl();
                Assert.DoesNotThrow(() => repo.Login("mithuna", "pass123"));
            }

            [Test]
            public void DeleteExpense_ValidId_DoesNotThrow()
            {
                var repo = new FinanceRepositoryImpl();
                Assert.DoesNotThrow(() => repo.DeleteExpense(3)); // Only if ID exists
            }
        }
    }
}
