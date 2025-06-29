namespace FinanceManagement.Entities
{
    public class Expense
    {
         int expenseId;
         int userId;
         decimal amount;
         int categoryId;
         DateTime date;
         string description;

        //Default constructor
        public Expense() { }

        //Parametrized constructor
        public Expense(int expenseId, int userId, decimal amount, int categoryId, DateTime date, string description)
        {
            this.expenseId = expenseId;
            this.userId = userId;
            this.amount = amount;
            this.categoryId = categoryId;
            this.date = date;
            this.description = description;
        }

        //Getters and setters
        public int ExpenseId
        {
            get { return expenseId; }
            set { expenseId = value; }
        }

        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        public decimal Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        public int CategoryId
        {
            get { return categoryId; }
            set { categoryId = value; }
        }

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        //To string method
        public override string ToString()
        {
            return $"ExpenseId: {expenseId}, Amount: {amount}, Date: {date.ToShortDateString()}, CategoryId: {categoryId}, Description: {description}";
        }
    }
}
