﻿using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using excel = Microsoft.Office.Interop.Excel;

namespace Selenium
{
    internal class AddToDoItemsToListTests
    {
        private IWebDriver _webDriver;

        [SetUp]
        public void SetupBrowser()
        {
            _webDriver = new ChromeDriver();
        }

        [Test]
        public void AddToDoItemToListTest()
        {
            const string expected = "Test to do item #1";

            _webDriver.Navigate().GoToUrl("https://localhost:44312/");
            var wait = new WebDriverWait(_webDriver, System.TimeSpan.FromSeconds(10));

            _webDriver.FindElement(By.XPath(".//a[contains(text(),'Login')]")).Click();

            wait.Until(driver => driver.FindElement(By.XPath(".//input[@id='Input_Email']")));

            _webDriver.FindElement(By.XPath(".//input[@id='Input_Email']")).SendKeys("test1@gmail.com");
            _webDriver.FindElement(By.XPath(".//input[@id='Input_Password']")).SendKeys("Test12345!");
            _webDriver.FindElement(By.XPath(".//button[@id='login-submit']")).Click();
            _webDriver.Navigate().GoToUrl("https://localhost:44312/todo");

            wait.Until(driver => driver.FindElement(By.XPath(".//li/div")));

            _webDriver.FindElement(By.XPath(".//li/div")).Click();
            _webDriver.FindElement(By.XPath(".//ul[@id='todo-items']/li")).Click();
            _webDriver.FindElement(By.XPath(".//input[@id='itemTitle0']")).SendKeys(expected);
            _webDriver.FindElement(By.XPath(".//app-root/body")).Click();

            string actual = _webDriver.FindElement(By.XPath(".//ul[@id='todo-items']/li/div/div[2]/div/span")).Text;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DataDrivenTest()
        {
            var excelApp = new excel.Application();
            var workbook = excelApp.Workbooks.Open("c:/users/mrkon/desktop/Test.xlsx");
            excel._Worksheet workSheet = workbook.Sheets[1];
            var valueRange = workSheet.UsedRange;

            _webDriver.Navigate().GoToUrl("https://localhost:44312/");
            var wait = new WebDriverWait(_webDriver, System.TimeSpan.FromSeconds(10));

            _webDriver.FindElement(By.XPath(".//a[contains(text(),'Login')]")).Click();

            wait.Until(driver => driver.FindElement(By.XPath(".//input[@id='Input_Email']")));

            _webDriver.FindElement(By.XPath(".//input[@id='Input_Email']")).SendKeys("test1@gmail.com");
            _webDriver.FindElement(By.XPath(".//input[@id='Input_Password']")).SendKeys("Test12345!");
            _webDriver.FindElement(By.XPath(".//button[@id='login-submit']")).Click();
            _webDriver.Navigate().GoToUrl("https://localhost:44312/todo");

            wait.Until(driver => driver.FindElement(By.XPath(".//li/div")));

            _webDriver.FindElement(By.XPath(".//li/div")).Click();

            int j = 1;

            for (int i = 2; i <= valueRange.Count; i++)
            {
                string expected = valueRange.Cells[1][i].value2;

                _webDriver.FindElement(By.XPath(".//button[contains(.,'Add Item...')]")).Click();
                _webDriver.FindElement(By.XPath($".//input[@id='itemTitle{j}']")).SendKeys(expected);
                _webDriver.FindElement(By.XPath(".//app-root/body")).Click();

                string actual = _webDriver.FindElement(By.XPath($".//ul[@id='todo-items']/li[{j + 1}]/div/div[2]/div/span")).Text;
                Assert.AreEqual(expected, actual);

                j++;
            }

            excelApp.Quit();
            workSheet = null;
            workbook = null;
            excelApp = null;
        }

        [TearDown]
        public void LogoutAndCloseBrowser()
        {
            var logoutLink = _webDriver.FindElement(By.XPath(".//a[contains(text(),'Logout')]"));
            logoutLink.Click();

            _webDriver.Close();
        }
    }
}