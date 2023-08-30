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

        public string GetMealOverExpensesMarker()
        {
            var mealOverExpensesMarker =
                ExpenseType == ExpenseType.Dinner && Amount > 5000 ||
                ExpenseType == ExpenseType.Breakfast && Amount > 1000
                    ? "X"
                    : " ";
            return mealOverExpensesMarker;
        }

        public bool IsMeal()
        {
            return ExpenseType == ExpenseType.Dinner ||
                   ExpenseType == ExpenseType.Breakfast;
        }

        public override string ToString()
        {
            return GetExpenseName() + "\t" + Amount + "\t" + GetMealOverExpensesMarker();
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
                if (expense.IsMeal()) {
                    mealExpenses += expense.Amount;
                }

                Console.WriteLine(expense.ToString());

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