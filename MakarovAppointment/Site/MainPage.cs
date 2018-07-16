using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
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
        By MainPopup { get; } = By.CssSelector("#fancybox-wrap");
        By _selectorLogin { get; } = By.CssSelector("#modlgn-username");
        By _selectorPassword { get; } = By.CssSelector("#modlgn-passwd");

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
            try
            {
                IWebElement fancyBox = Driver.FindElement(MainPopup);
                MainPageMsg = fancyBox.Text;
            }
            catch { return; }

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
}