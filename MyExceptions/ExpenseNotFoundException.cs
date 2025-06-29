using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagement.MyExceptions
{
    public class ExpenseNotFoundException : Exception
    {
        public ExpenseNotFoundException() : base("Expense not found.") { } // default error message

        public ExpenseNotFoundException(string message) : base(message) { }// custom exception
    }
}
