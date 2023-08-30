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
            var expenses = new List<Expense>()
            {
                new Expense(){ amount = 1, type = ExpenseType.DINNER, },
            };
            using (var sw = new StringWriter()) {
                Console.SetOut(sw);
                expenseReport.PrintReport(expenses);
                var sb = new StringBuilder();
                sb.AppendLine("Expenses NOW");
                sb.AppendLine("Expenses NOW");
                
                Assert.AreEqual($"hello{Environment.NewLine}", sw.ToString());
                
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
}