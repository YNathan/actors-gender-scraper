using System.Collections.Generic;
using System.Threading.Tasks;
using CelebritiesSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CelebritiesSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataFeedController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IDataFeedService _dataFeedService;
        
        public DataFeedController(ILogger<WeatherForecastController> logger, IDataFeedService dataFeedService)
        {
            _logger = logger;
            _dataFeedService = dataFeedService;
        }
        
        [HttpGet]
        public async Task<List<CelebrityResponseDto>> Get()
        {
            return await _dataFeedService.getCelebrities();
        }
        
        // https://localhost:5001/DataFeed?name=Daniel Radcliffe
        [HttpDelete]
        public List<CelebrityResponseDto> Delete(string name)
        {
            return _dataFeedService.deleteCelebrityByName(name);
        }
    }
}