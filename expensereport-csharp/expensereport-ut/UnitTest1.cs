using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using expensereport_csharp;
using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test2()
        {
            var expenseReport = new ExpenseReport();
            var fakeTimeProvider = new FakeTimeProvider();
            var expenses = new List<Expense>()
            {
                new Expense(){ Amount = 1, ExpenseType = ExpenseType.Dinner, },
                new Expense(){ Amount = 5001, ExpenseType = ExpenseType.Dinner, },
                new Expense(){ Amount = 1, ExpenseType = ExpenseType.Breakfast, },
                new Expense(){ Amount = 1001, ExpenseType = ExpenseType.Breakfast, },
                new Expense(){ Amount = 1, ExpenseType = ExpenseType.CarRental, },
            };
            using (var sw = new StringWriter()) {
                Console.SetOut(sw);
                expenseReport.PrintReport(expenses, fakeTimeProvider);
                var sb = new StringBuilder();
                sb.AppendLine("Expenses " + fakeTimeProvider.Now);
                sb.AppendLine("Dinner\t1\t ");
                sb.AppendLine("Dinner\t5001\tX");
                sb.AppendLine("Breakfast\t1\t ");
                sb.AppendLine("Breakfast\t1001\tX");
                sb.AppendLine("Car Rental\t1\t ");
                sb.AppendLine("Meal expenses: 6004");
                sb.AppendLine("Total expenses: 6005");
                
                Assert.AreEqual(sb.ToString(), sw.ToString());
                
                //"Expenses "+ DateTime.Now
                // expenseName + "\t" + expense.amount + "\t" + mealOverExpensesMarker
                // Meal expenses: " + mealExpenses
                // Total expenses: " + total
            }
        }

        [Test]
        public void Test1()
        {
            var expenseReport = new ExpenseReport();
            using (var sw = new StringWriter()) {
                Console.SetOut(sw);
                expenseReport.Print();
                Assert.AreEqual($"hello{Environment.NewLine}", sw.ToString());
            }
        }
    }
    
    public class FakeTimeProvider: INowProvider
    {
        public DateTime Now => DateTime.MinValue;
    }
}