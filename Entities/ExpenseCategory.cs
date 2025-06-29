namespace FinanceManagement.Entities
{
    public class ExpenseCategory
    {
         int categoryId;
         string categoryName;

        // Default constructor
        public ExpenseCategory() { }

        //Parameterized constructor
        public ExpenseCategory(int categoryId, string categoryName)
        {
            this.categoryId = categoryId;
            this.categoryName = categoryName;
        }

        //getters and setters
        public int CategoryId
        {
            get { return categoryId; }
            set { categoryId = value; }
        }

        public string CategoryName
        {
            get { return categoryName; }
            set { categoryName = value; }
        }
        public override string ToString()
        {
            return $"CategoryId: {categoryId}, CategoryName: {categoryName}";
        }
    }
}
    

