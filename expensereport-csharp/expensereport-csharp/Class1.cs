using System;
using System.Collections.Generic;

namespace expensereport_csharp
{
    public enum ExpenseType
    {
        Dinner, 
        Breakfast, 
        CarRental,
        Lunch,
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
                case ExpenseType.Lunch:
                    return "Lunch";
                case ExpenseType.CarRental:
                    return "Car Rental";
            }
            return String.Empty;
        }

        public string GetMealOverExpensesMarker()
        {
            var mealOverExpensesMarker =
                ExpenseType == ExpenseType.Dinner && Amount > 5000 ||
                ExpenseType == ExpenseType.Lunch && Amount > 2000 ||
                ExpenseType == ExpenseType.Breakfast && Amount > 1000
                    ? "X"
                    : " ";
            return mealOverExpensesMarker;
        }

        public bool IsMeal()
        {
            return ExpenseType == ExpenseType.Dinner ||
                   ExpenseType == ExpenseType.Lunch ||
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
            Console.WriteLine("Expenses " + timeProvider.Now);
            
            foreach (Expense expense in expenses) {
                Console.WriteLine(expense.ToString());
            }
            
            var expenseDetails = CalculateExpenseDetails(expenses);

            Console.WriteLine(expenseDetails.ToString());
        }

        private ExpenseDetails CalculateExpenseDetails(List<Expense> expenses)
        {
            var mealExpenses = 0;
            var total = 0;
            foreach (Expense expense in expenses) {
                if (expense.IsMeal()) {
                    mealExpenses += expense.Amount;
                }
                total += expense.Amount;
            }
            return new ExpenseDetails {
                Total = total,
                MealExpenses = mealExpenses
            };
        }

        public void Print()
        {
            Console.WriteLine("hello");
        }
    }

    public class DateTimeProvider : INowProvider
    {
        public DateTime Now => DateTime.Now;
    }

    public interface INowProvider
    {
        DateTime Now { get; }
    }

    public struct ExpenseDetails
    {
        public int Total;
        public int MealExpenses;

        public override string ToString()
        {
            return $"Meal expenses: {MealExpenses}{Environment.NewLine}Total expenses: {Total}"; 
        }
    }
}