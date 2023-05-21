﻿using Microsoft.Playwright;
using PlayWriteApplication;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;
using System.Diagnostics.Metrics;

//var exitCode = Microsoft.Playwright.Program.Main(new[] { "install" });
var openAiKey = Environment.GetEnvironmentVariable("OpenAI_token");

var hh = new HhPages("https://hh.ru");
var login = "Light010191@yandex.ru";
var password = "424Light080";
var vacancy = "разработчик junior C#";

using var playwright = await Playwright.CreateAsync();
await using IBrowser browser = await playwright.Chromium.LaunchAsync(
    new BrowserTypeLaunchOptions
    {
        Headless = false,
        SlowMo =200,
        Timeout = 30000,       
    }) ;

BrowserNewContextOptions browsreOptions = playwright.Devices["Desktop Chrome"];
await using IBrowserContext context = await browser.NewContextAsync(browsreOptions);
context.SetDefaultTimeout(50000);
IPage page = await context.NewPageAsync();
await Login();

Console.ReadKey();
//"search-input"
async Task Login()
{
    await page.GotoAsync(hh.Login);
    
    await page.ClickAsync(hh.ButtonQa("expand-login-by-password"));
    await page.TypeAsync(hh.InputQa("login-input-username"),  login);
    await page.TypeAsync(hh.InputQa("login-input-password"),  password);
    await page.ClickAsync(hh.ButtonQa("account-login-submit"));

    var incorrectPassword = await page.IsVisibleAsync(hh.DivQa("account-login-error"));
    if (incorrectPassword)        throw new Exception("Incorrect login or password"); 

    await page.TypeAsync(hh.InputQa("search-input"), vacancy);
    await page.ClickAsync(hh.ButtonQa("search-button"));
    var val = await page.TextContentAsync(hh.CountVacancysQa());

    int countVacancys;
    int.TryParse(string.Join("", val.Where(c => char.IsDigit(c))), out countVacancys);
    if (countVacancys == 0) throw new Exception("вакансий не найдено");

    await page.ClickAsync(hh.RefQa("vacancy-serp__vacancy_response"));

    //var vacancyAbroad = await page.IsVisibleAsync(hh.SpanRelocate());
    //if (vacancyAbroad)
    //{
    //    await page.ClickAsync(hh.ButtonQa("relocation-warning-abort"));
    //}

    string userMassage =$"Напиши небольшое сопроводительное письмо для вакансии{vacancy} ";      
    ChatGptClient chatGptClient = new ChatGptClient(openAiKey);
    string message = await chatGptClient.GetChatGptMessage(userMassage);
    await page.TypeAsync(hh.TextareaQa("vacancy-response-popup-form-letter-input"),message);


    //var vacancys = await page.GetAttributeAsync(hh.RefQa("vacancy-serp__vacancy_response"),"href");
    //while (countVacancys>0)
    //{
    //   vacancys.Add(await page.GetAttributeAsync("div.serp-item-controls > a ", "href"));

    //    countVacancys--;
    //}

}
