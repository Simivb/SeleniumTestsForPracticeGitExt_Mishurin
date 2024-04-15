using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumTest_Mishurin;

public class SelenuimTestsForPractice
{
    [Test]
    public void Authorization()
    {
        var options = new ChromeOptions();
        options.AddArguments("--no-sandbox", "--start-maximized", "--disable-extensions");
        
        // зайти в хром
        var driver = new ChromeDriver(options);
        
        // перейти по урлу https://staff-testing.testkontur.ru
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru");
        Thread.Sleep(5000);

        // ввести логин и пароль
        var login = driver.FindElement(By.Id("Username"));
        login.SendKeys("mishurin.ivan@gmail.com");

        var password = driver.FindElement(By.Name("Password"));
        password.SendKeys("78h46j3014Ff.@123");
        Thread.Sleep(5000);

        //нажать на кнопку "войти"
        var enter = driver.FindElement(By.Name("button"));
        enter.Click();
        Thread.Sleep(5000);
        
        // проверить, что перешли по нужной ссылке
        var currentUrl = driver.Url;
        Assert.That(currentUrl == "https://staff-testing.testkontur.ru/news");
        
        //закрываем браузер и убиваем процесс драйвера
        driver.Quit();
    }
}