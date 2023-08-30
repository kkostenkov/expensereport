using System;
using System.IO;
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
        public void Test1()
        {
            var expenseReport = new ExpenseReport();
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                expenseReport.Print();
                Assert.AreEqual("hello\r\n", sw.ToString());
            }
            
            Assert.Pass();
        }
    }
}