using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using CelebritiesSystem.Services;
using Microsoft.Extensions.Logging;


namespace CelebritiesSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScraperController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IScraperService _scraperService;
        private readonly IDataBaseService _dataBaseService;
        
        public ScraperController(ILogger<WeatherForecastController> logger,IScraperService scraperService, IDataBaseService dataBaseService)
        {
            _logger = logger;
            _scraperService = scraperService;
            _dataBaseService = dataBaseService;
        }
        [HttpGet]
        public async Task<List<CelebrityResponseDto>> initilizeData()
        {
            await _scraperService.InitializeData();
            return  _dataBaseService.getAllCelebrities();;
        }
    }
}