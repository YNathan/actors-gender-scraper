using System.Collections.Generic;
using System.Threading.Tasks;

namespace CelebritiesSystem.Services
{
    public interface IDataFeedService
    {
        Task<List<CelebrityResponseDto>> getCelebrities();
        List<CelebrityResponseDto> deleteCelebrityByName(string name);
        
    }
}