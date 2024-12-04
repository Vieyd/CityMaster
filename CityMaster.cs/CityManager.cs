using System;
using System.Collections.Generic;
using System.Linq;
using КурсоваяРабота;

namespace CityMaster
{
    public class CityManager
    {
        private List<string> cities;

        public CityManager()
        {
            // Инициализируем список городов из класса CityData
            cities = CityData.Cities;
        }

        // Проверка наличия города
        public bool ContainsCity(string city)
        {
            return cities.Any(c => string.Equals(c, city, StringComparison.OrdinalIgnoreCase));
        }

        // Получение списка городов, начинающихся с определенной буквы, исключая использованные
        public List<string> GetCitiesStartingWith(char letter, HashSet<string> excludeCities)
        {
            return cities.Where(c => char.ToLower(c[0]) == char.ToLower(letter) &&
                                     !excludeCities.Contains(c, StringComparer.OrdinalIgnoreCase))
                         .ToList();
        }

        // Получение всех городов
        public List<string> GetAllCities()
        {
            return new List<string>(cities);
        }
    }
}
