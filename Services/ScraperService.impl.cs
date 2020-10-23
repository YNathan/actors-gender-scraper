using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using CelebritiesSystem;
using CelebritiesSystem.Services;

public class ScraperService_impl : IScraperService
{
    private IConfiguration config;
    private IBrowsingContext context;
    private readonly IDataBaseService _dataBaseService;
    public Boolean isScraped;
    public ScraperService_impl(IDataBaseService dataBaseService)
    {
         config = Configuration.Default.WithDefaultLoader();
         context = BrowsingContext.New(config);
         _dataBaseService = dataBaseService;
         isScraped = false;
    }

    public async Task<ArrayList> ScrapeCelebritiesUrlsList()
    {
        ArrayList celebsList = new ArrayList();
        var document = await context.OpenAsync("https://www.imdb.com/list/ls052283250/");
        var celebritiesElements = document.QuerySelectorAll(".lister-item-image");
        foreach (var celebElement in celebritiesElements)
        {
            celebsList.Add((celebElement.ChildNodes.ToArray()[1] as IHtmlAnchorElement).Href);
            Console.WriteLine((celebElement.ChildNodes.ToArray()[1] as IHtmlAnchorElement).Href);
        }
            
        // Log the data to the console
        //  _logger.LogInformation(document.DocumentElement.OuterHtml);
        return celebsList;
    }

    public async Task<CelebrityResponseDto> ScrapeCelebritiesDetailsByUrl(string url)
    {
        var document = await context.OpenAsync(url);
        CelebrityResponseDto celebrity = new CelebrityResponseDto();
        
        // name section
        var nameElements = document.QuerySelector(".name-overview-widget__section").QuerySelector(".header").QuerySelector(".itemprop");
        Console.WriteLine(nameElements.TextContent);
        
        // DateOfBirth section 
        var dateOfBirthElements = document.QuerySelector("#name-born-info");
        var birthDateTimes = (dateOfBirthElements.ChildNodes[3] as IHtmlTimeElement).DateTime.Split("-");
        var dayInMonth =  int.Parse(birthDateTimes[2]);
        var month = int.Parse(birthDateTimes[1]);
        var year = int.Parse(birthDateTimes[0]);
        
        // role section
        ArrayList roles = new ArrayList();
        var rolsElements = document.QuerySelector(".infobar").QuerySelectorAll(".itemprop");
        foreach (var roleElement in rolsElements)
        {
            foreach (var role in roleElement.ChildNodes)
            {
                Console.WriteLine(role.TextContent);
                roles.Add(role.TextContent);
            }
        }
        
        // image url section 
        var imagesUrlElement = document.QuerySelector(".poster-hero-container").QuerySelector(".image");
        var imageUrl = (imagesUrlElement.ChildNodes[1].ChildNodes[1] as IHtmlImageElement).Source;
        
        celebrity.Name = nameElements.TextContent;
        celebrity.Roles = (from string x in roles select x).ToArray();
        celebrity.DateOfBirth = new DateTime(year,month,dayInMonth,0,0,0);
        celebrity.ImageUrl = imageUrl;
        celebrity.Gender = getGenderFromRols(celebrity.Roles);
        _dataBaseService.storeNewCelebrityInDataBase(celebrity);
        return celebrity;

    }

    private Gender getGenderFromRols(string[] rols)
    {
        // consider actor/ actreess maybe.. need to investigate,
        // now iam thinking maybe from the 'read more bio' looking with regex all occurence ', She' | '. She'
        // in male we never start a senstance like that because if its a 'she' that we refere to it will be in
        // the middle of a sentence because its a female that the sentence start with
        return Gender.MALE;
    }

    public async Task InitializeData()
    {
        var urls = await this.ScrapeCelebritiesUrlsList();
        var tasks = new List<Task>();
        
        foreach (var url in urls)
        {
            tasks.Add(this.ScrapeCelebritiesDetailsByUrl(url as string));
        }
        
        await Task.WhenAll(tasks);
        isScraped = true;

    }
    
}