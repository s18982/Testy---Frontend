using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FrontendTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] logins = { "correctLogin", "wrongLogin" };
            string[] passwords = { "correctPass", "wrongPass"};

            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://localhost:3000/login");
            driver.Manage().Window.Maximize();

            Thread.Sleep(6000);

            foreach(string login in logins){
                driver.FindElement(By.XPath("//*[@id='username']")).Clear();
                driver.FindElement(By.XPath("//*[@id='username']")).SendKeys(login);

                foreach(string pass in passwords)
                {
                    driver.FindElement(By.XPath("//*[@id='password']")).Clear();
                    driver.FindElement(By.XPath("//*[@id='password']")).SendKeys(pass);
                    driver.FindElement(By.XPath("//*[@id='root']/div/form/div[3]/button")).Click();
                    Thread.Sleep(2000);                    
                }
            }            
            driver.FindElement(By.XPath("//*[@id='basic-navbar-nav']/div/a[9]")).Click();
            Thread.Sleep(5000);

            Dictionary<string, string> linePaths = new Dictionary<string, string>();
            linePaths.Add("imie", "//*[@id='firstName']");
            linePaths.Add("nazwisko", "//*[@id='lastName']");
            linePaths.Add("email", "//*[@id='email']");
            linePaths.Add("nazwa", "//*[@id='username']");
            linePaths.Add("haslo", "//*[@id='password']");
            linePaths.Add("data", "//*[@id='birthDate']");
            linePaths.Add("telefon", "//*[@id='phoneNumber']");

            string[] words = { "Firstname", "LastName", "email@test.pl", "username123", "P@ssw0rd", "15.08.1918", "623456876" };
            Dictionary<string, string> wordsDictionary = new Dictionary<string, string>();
            wordsDictionary.Add("imie", words[0]);
            wordsDictionary.Add("nazwisko", words[1]);
            wordsDictionary.Add("email", words[2]);
            wordsDictionary.Add("nazwa", words[3]);
            wordsDictionary.Add("haslo", words[4]);
            wordsDictionary.Add("data", words[5]);
            wordsDictionary.Add("telefon", words[6]);

            driver.FindElement(By.CssSelector("#basic-navbar-nav > div > a:nth-child(3)")).Click();

            driver.FindElement(By.XPath(linePaths["imie"])).SendKeys(wordsDictionary["imie"]);
            driver.FindElement(By.XPath(linePaths["nazwisko"])).SendKeys(wordsDictionary["nazwisko"]);
            driver.FindElement(By.XPath(linePaths["email"])).SendKeys(wordsDictionary["email"]);
            driver.FindElement(By.XPath(linePaths["nazwa"])).SendKeys(wordsDictionary["nazwa"]);
            driver.FindElement(By.XPath(linePaths["haslo"])).SendKeys(wordsDictionary["haslo"]);
            driver.FindElement(By.XPath(linePaths["data"])).SendKeys(wordsDictionary["data"]);
            driver.FindElement(By.XPath(linePaths["telefon"])).SendKeys(wordsDictionary["telefon"]);

            foreach (string key in linePaths.Keys)
            {             
                foreach (string str in wordsDictionary.Keys)
                {
                    driver.FindElement(By.XPath(linePaths[key])).Clear();
                    driver.FindElement(By.XPath(linePaths[key])).SendKeys(wordsDictionary[str]);

                    Thread.Sleep(300);
                }
                driver.FindElement(By.XPath(linePaths[key])).Clear();
                driver.FindElement(By.XPath(linePaths[key])).SendKeys(wordsDictionary[key]);
            }

            Thread.Sleep(6000);

            driver.Quit();
        }
    }
}
