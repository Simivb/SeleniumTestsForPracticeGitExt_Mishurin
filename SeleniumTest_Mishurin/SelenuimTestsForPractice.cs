using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SeleniumTest_Mishurin;

public class SelenuimTestsForPractice
{
    public ChromeDriver driver;

    [SetUp]
    public void Setup()
    {
        //Устанавливаем параметры для запуска хрома
        var options = new ChromeOptions();
        options.AddArguments("--no-sandbox", "--start-maximized", "--disable-extensions");
        driver = new ChromeDriver(options);
        //Устанавливаем неявное ожидание при каждом вызове драйвера
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5); // неявное ожидание
        //Авторизуемся
        Authorization();
    }
    public void Authorization()
    {
        //Переходим по ссылке https://staff-testing.testkontur.ru
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru");
        //Находим поле логин
        var login = driver.FindElement(By.Id("Username"));
        //Вводим логин
        login.SendKeys("mishurin.ivan@gmail.com");
        //Находим поле пароля
        var password = driver.FindElement(By.Name("Password"));
        //Вводим пароль
        password.SendKeys("Qwerty321@");
        //Находим кнопку "Войти"
        var enter = driver.FindElement(By.Name("button"));
        //Нажимаем кнопку "Войти"
        enter.Click();
        //Ждем, пока прогрузится страница новостей
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        wait.Until(ExpectedConditions.UrlToBe("https://staff-testing.testkontur.ru/news"));
    }

    [Test] 
    public void AuthorizationTest()
    {
        //Ищем на странице надпись "Новости", параллельно дожидаясь загрузки странцицы
        var news = driver.FindElement(By.CssSelector("[data-tid='Feed']"));
        //Получаем урл страницы
        var currentUrl = driver.Url;
        //Проверяем, что урл правильный
        currentUrl.Should().Be("https://staff-testing.testkontur.ru/news");
    }


    [Test]
    public void NavigationTest()
    {
        //Ищем на странице кнопку "Сообщества"
        var community = driver.FindElement(By.CssSelector("[data-tid='Community']"));
        //Нажимаем на кнопку "Сообщества" 
        community.Click();
        //Ищем на странице "Сообщества", параллельно ожидая загрузки страницы
        var communityTitle = driver.FindElement(By.CssSelector("[data-tid='Title']"));
        //Проверяем, что урл правильный
        driver.Url.Should().Be("https://staff-testing.testkontur.ru/communities");
    }

    [Test]
    public void CreateEvent()
    {
        // Переходим по ссылке https://staff-testing.testkontur.ru/events
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru/events");
        // Находим кнопку "Создать" для создания мероприятия
        var buttonCreateEvent = driver.FindElement(By.ClassName("react-ui-1cc0mqn"));  
        // Нажимаем на кнопку создать
        buttonCreateEvent.Click();
        // Находим поле названия мероприятия
        var eventName = driver.FindElement(By.ClassName("react-ui-seuwan"));
        // Вводим название мероприятия
        eventName.SendKeys("Название мероприятия");
        // Находим поле ИНН
        var inn = driver.FindElements(By.ClassName("react-ui-g51x6v")).ElementAt(1);
        // Вводим ИНН
        inn.SendKeys("000000000000");
        // Находим календарь
        var calendar = driver.FindElements(By.ClassName("react-ui-11i844s")).ElementAt(2);
        // Нажимаем на кнопку календаря
        calendar.Click();
        // Находим нужную дату(30.04.2024)
        var date = driver.FindElements(By.ClassName("react-ui-1ty8tof")).ElementAt(21);
        // Нажимаем на дату
        date.Click();
        // Находим кнопку создать
        var buttonCreate2 = driver.FindElement(By.ClassName("react-ui-1m5qr6w"));
        // Нажимаем кнопку создать
        buttonCreate2.Click();
        // Находим кнопку закрыть
        var close = driver.FindElement(By.CssSelector(".sc-juXuNZ.kVHSha"));
        // Нажимаем кнопку закрыть
        close.Click();
        // Сохраняем в переменную страницу мероприятия
        var url = driver.Url.Substring(35);
        // Переходим на страницу всех мероприятий
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru/events");
        // Ищем с помощью урла наше мероприятие
        var myEvent = driver.FindElement(By.CssSelector($"a[href='{url}']"));
        //Мероприятие должно существовать
        myEvent.Should().NotBeNull();
    }
    
    [Test]
    public void CreateDiscussion()
    {
        //Записываем тему обсуждения в переменную
        var theme = "Тема обсуждения";
        //Переходим по ссылке заранее созданного мероприятия для создания обсуждения
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru/events/" +
                                      "e89e02f0-b999-4d46-af0c-7b6bde8bf0c4?tab=discussions");
        // Находим кнопку "Создать обсуждение"
        var buttonCreateDisc = driver.FindElement(By.ClassName("react-ui-1r9zj4l"));
        // Нажимаем на кнопку "Создать обсуждение"
        buttonCreateDisc.Click();
        //Находим поле "Тема обсуждения"
        var nameOfDisc = driver.FindElement(By.XPath("/html/body/div[2]/div/div[2]/div/div/div/div/" +
                                                     "div[3]/div[2]/div/span/div/div[2]/div[1]/span/label"));
        // Заполняем поле нашей темой обсуждения
        nameOfDisc.SendKeys(theme);
        // Находим элемент "Сообщение"
        var message = driver.FindElement(By.XPath("/html/body/div[2]/div/div[2]/div/div/div/div/" +
                                                  "div[3]/div[2]/div/span/div/div[4]/span/div/div/div[2]/div/div/div"));
        // Заполняем поле "Сообщение"
        message.SendKeys("Сообщение");
        // Находим кнопку создания
        var buttonCreate = driver.FindElement(By.ClassName("react-ui-1m5qr6w"));
        // Нажимаем кнопку создания
        buttonCreate.Click();
        // Ищем на странице созданное обсуждение
        var myDisc = driver.FindElement(By.XPath("/html/body/div/section/section[2]/div/div/div/section/" +
                                                 "div/div/div/div/div/a[2]"));
        //Проверяем, что тема обсуждения совпадает с нужной
        myDisc.Text.Should().Be(theme);
    }
    
    [Test]
    public void CreateComment()
    {
        //Записываем наш комментарий в переменную
        var comment = "Осмысленный комментарий из пяти и более слов для продвижения ролика";
        //Переходим по ссылке в обсуждение для создания комментария
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru/events/e89e02f0-b999-4d46-af0c-7b6bde8bf0c4?" +
                                  "tab=discussions&id=7aaeca3c-8fdf-48ce-a8fe-2e9fafe81134");
        //Находим кнопку "Комментировать"
        var createComment = driver.FindElement(By.XPath("/html/body/div/section/section[2]/div/div/div/" +
                                                        "section/div[4]/button"));;
        //Нажимаем на кнопку "Комментировать"
        createComment.Click();
        //Находим поле для ввода символов
        var message = driver.FindElement(By.ClassName("react-ui-r3t2bi"));
        //Вводим в поле для ввода наш комментарий
        message.SendKeys(comment);
        //Находим кнопку "Отправить"
        var sendCommentButton = driver.FindElement(By.ClassName("react-ui-m0adju"));
        //Нажимаем на кнопку "Отправить"
        sendCommentButton.Click();
        //Находим наш комментарий
        var myComment = driver.FindElement(By.CssSelector("[data-tid='TextComment']"));
        //Проверяем, что наш комментарий существует
        myComment.Text.Should().Be(comment);
    }
    
    [TearDown]
        
    public void TearDown()
    {
        driver.Quit();
    }
}
//Была возможность везде вместо поиска по ClassName искать по XPath, но мне не нравится, как это выглядит 