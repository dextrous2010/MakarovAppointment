using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MakarovAppointment.Site
{
    class MainPage
    {
        readonly string _siteUri = "http://doctor-makarov.com.ua/";
        IWebDriver Driver;
        WebDriverWait DriverWait;
        By MainPopup { get; } = By.CssSelector("#fancybox-wrap");
        By _selectorLogin { get; } = By.CssSelector("#modlgn-username");
        By _selectorPassword { get; } = By.CssSelector("#modlgn-passwd");
        By _btnSubmit { get; } = By.CssSelector("div > #form-login-submit > div > button");
        
        public string MainPageMsg { get; set; }

        public MainPage()
        {
            OpenSite();
            CheckFancyBox();
        }

        public void OpenSite()
        {
            Driver = new ChromeDriver();
            Driver.Manage().Window.Maximize();

            try
            {
                Driver.Navigate().GoToUrl(_siteUri);
            }
            catch (Exception e)
            {
                Driver.Quit();
                MessageBox.Show($"Could not navigate to {_siteUri}. ERROR: {e.Message}");
            }

            if (!Driver.Url.Equals(_siteUri))
            {
                Driver.Quit();
                MessageBox.Show($"Could not navigate to {_siteUri}. The following URI is opened: {Driver.Url}");
            }
        }

        public void CheckFancyBox()
        {
            bool fancyBoxAppeared = false;
            try
            {
                IWebElement fancyBox = Driver.FindElement(MainPopup);
                MainPageMsg = fancyBox.Text;
                if (!string.IsNullOrEmpty(MainPageMsg))
                    fancyBoxAppeared = true;
            }
            catch { return; }

            if (fancyBoxAppeared)
            {
                try
                {
                    //Trying to close the fancybox
                    Driver.FindElement(By.CssSelector("#fancybox-close")).Click();
                }
                catch (Exception)
                {
                    throw;
                }

            }
        }

        public void DoLogin(string login, string password)
        {
            IWebElement loginEl;
            IWebElement passwordEl;

            try
            {
                loginEl = Driver.FindElement(_selectorLogin);
                passwordEl = Driver.FindElement(_selectorPassword);
                loginEl.SendKeys(login);
                passwordEl.SendKeys(password);
                System.Threading.Thread.Sleep(500);
                Driver.FindElement(_btnSubmit).Click();
            }
            catch (Exception)
            {
                throw;
            }

            System.Threading.Thread.Sleep(500);
            CheckFancyBox();
        }

        public void MakeAnAppointment()
        {
            try
            {
                Driver.Navigate().GoToUrl("http://doctor-makarov.com.ua/zapis-na-priem");
            }
            catch (Exception)
            {

                throw;
            }

            try
            {
                Driver.FindElement(By.CssSelector("#centercontent_md > div.ttfspspec > table > tbody > tr:nth-child(1) > td:nth-child(4) > div > a")).Click();
                DriverWait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
                DriverWait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#centercontent_md > div.ttfsptime > table:nth-child(1) > tbody > tr > td:nth-child(2) > div.fiospec")));
            }
            catch (Exception)
            {

                throw;
            }


            
        }
    }
}