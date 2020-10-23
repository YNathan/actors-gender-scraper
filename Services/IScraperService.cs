using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CelebritiesSystem.Services
{
    public interface IScraperService
    {
        Task<ArrayList> ScrapeCelebritiesUrlsList();
        Task<CelebrityResponseDto> ScrapeCelebritiesDetailsByUrl(string url);

        Task InitializeData();
    }
}