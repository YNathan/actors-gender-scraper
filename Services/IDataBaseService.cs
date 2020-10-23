using System.Collections.Generic;

namespace CelebritiesSystem.Services
{
     
    public interface IDataBaseService
    {
        void storeNewCelebrityInDataBase(CelebrityResponseDto celebrity);
        void deleteCelebrityByName(string name);
        List<CelebrityResponseDto> getAllCelebrities();
        void syncData();

    }
}