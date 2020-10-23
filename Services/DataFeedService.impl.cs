using System.Collections.Generic;
using System.Threading.Tasks;
using CelebritiesSystem.Controllers;
using Microsoft.Extensions.Logging;

namespace CelebritiesSystem.Services
{
   
    public class DataFeedService_impl : IDataFeedService
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IScraperService _scraperService;
        private readonly IDataBaseService _dataBaseService;

        public DataFeedService_impl(ILogger<WeatherForecastController> logger, IScraperService scraperService, IDataBaseService dataBaseService)
        {
            _logger = logger;
            _scraperService = scraperService;
            _dataBaseService = dataBaseService;
        }

        public async Task<List<CelebrityResponseDto>> getCelebrities()
        {
            
            if (_dataBaseService.getAllCelebrities().Count == 0)
            {
                await _scraperService.InitializeData();    
            }
            
            return  _dataBaseService.getAllCelebrities();;
        }

        public List<CelebrityResponseDto> deleteCelebrityByName(string name)
        {
            _dataBaseService.deleteCelebrityByName(name);
            _dataBaseService.syncData();
            return _dataBaseService.getAllCelebrities();
        }
    }
}