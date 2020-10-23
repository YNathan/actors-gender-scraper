using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace CelebritiesSystem.Services
{
    public class DataBaseService_impl : IDataBaseService
    {
        Dictionary<string, CelebrityResponseDto> _celebrities;

        public DataBaseService_impl()
        {
            _celebrities = new Dictionary<string, CelebrityResponseDto>();
            var storedCelebs = GetCelebrities();
            foreach (var celeb in storedCelebs)
            {
                _celebrities.Add(celeb.Name,celeb);    
            }
        }
        
        public void storeNewCelebrityInDataBase(CelebrityResponseDto celebrity)
        {
            if (!_celebrities.ContainsKey(celebrity.Name))
            {
                _celebrities.Add(celebrity.Name,celebrity);
            }
        }

        public void deleteCelebrityByName(string name)
        {
            if (_celebrities.ContainsKey(name))
            {
                _celebrities.Remove(name);
            }
        }

        public List<CelebrityResponseDto> getAllCelebrities()
        {
            return _celebrities.Values.ToList();
        }

        public void syncData()
        {
            AddCelebrities(_celebrities.Values.ToArray());

        }
        
        public void AddCelebrity(CelebrityResponseDto newCelebrity)
        {
            var json = File.ReadAllText("./celebrities-database.json");
            var celebritis = JsonConvert.DeserializeObject<List<CelebrityResponseDto>>(json);
            celebritis.Add(newCelebrity);
            File.WriteAllText("./celebrities-database.json", JsonConvert.SerializeObject(celebritis));
        }
        
        public void AddCelebrities(CelebrityResponseDto[] celebrities)
        {
            File.WriteAllText("./celebrities-database.json", JsonConvert.SerializeObject(celebrities));
        }

        public CelebrityResponseDto GetCelebrity(string name)
        {
            var json = File.ReadAllText("./celebrities-database.json");
            var celebrities = JsonConvert.DeserializeObject<List<CelebrityResponseDto>>(json);
            var result = new CelebrityResponseDto();
            foreach (var c in celebrities)
            {
                if (c.Name == name)
                {
                    result = c;
                    break;
                }
            }
            return result;
        }
        
        public List<CelebrityResponseDto> GetCelebrities()
        {
            var json = File.ReadAllText("./celebrities-database.json");
            List<CelebrityResponseDto> celebrities =  new List<CelebrityResponseDto>();
            try
            {
                celebrities = JsonConvert.DeserializeObject<List<CelebrityResponseDto>>(json); 
            }
            catch (JsonException ex)
            {
                
            }

            
            
            return celebrities;
        }
    }
}