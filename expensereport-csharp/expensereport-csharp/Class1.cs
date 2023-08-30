using System;
using System.Collections.Generic;

namespace expensereport_csharp
{
    public enum ExpenseType
    {
        Dinner, 
        Breakfast, 
        CarRental
    }

    public struct Expense
    {
        public ExpenseType ExpenseType;
        public int Amount;

        public string GetExpenseName()
        {
            switch (ExpenseType) {
                case ExpenseType.Dinner:
                    return "Dinner";
                case ExpenseType.Breakfast:
                    return "Breakfast";
                case ExpenseType.CarRental:
                    return "Car Rental";
            }
            return String.Empty;
        }
    }

    public class ExpenseReport
    {
        public void PrintReport(List<Expense> expenses)
        {
            PrintReport(expenses, new DateTimeProvider());
        }

        public void PrintReport(List<Expense> expenses, INowProvider timeProvider)
        {
            int total = 0;
            int mealExpenses = 0;

            Console.WriteLine("Expenses " + timeProvider.Now);
            
            foreach (Expense expense in expenses) {
                if (expense.ExpenseType == ExpenseType.Dinner || expense.ExpenseType == ExpenseType.Breakfast) {
                    mealExpenses += expense.Amount;
                }
                
                var expenseName = expense.GetExpenseName();

                String mealOverExpensesMarker =
                    expense.ExpenseType == ExpenseType.Dinner && expense.Amount > 5000 ||
                    expense.ExpenseType == ExpenseType.Breakfast && expense.Amount > 1000
                        ? "X"
                        : " ";

                Console.WriteLine(expenseName + "\t" + expense.Amount + "\t" + mealOverExpensesMarker);

                total += expense.Amount;
            }

            Console.WriteLine("Meal expenses: " + mealExpenses);
            Console.WriteLine("Total expenses: " + total);
        }

        public void Print()
        {
            Console.WriteLine("hello");
        }
    }

    public class DateTimeProvider: INowProvider
    {
        public DateTime Now => DateTime.Now;
    }

    public interface INowProvider
    {
        DateTime Now { get; }
    }
}