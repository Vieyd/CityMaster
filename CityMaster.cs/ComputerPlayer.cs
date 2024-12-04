using System;
using System.Collections.Generic;

namespace CityMaster
{
    public class ComputerPlayer
    {
        public string Name { get; private set; }
        private static readonly Random random = new Random();

        // Список использованных городов
        public List<string> UsedCities { get; private set; }

        // Отслеживание хода
        public bool IsTurn { get; set; }

        public ComputerPlayer(string name)
        {
            Name = name;
            UsedCities = new List<string>();
            IsTurn = false; // По умолчанию не ходит первым
        }

        // Метод для выбора города компьютером.

        public string MakeMove(char requiredLetter, List<string> availableCities)
        {
            if (availableCities == null || availableCities.Count == 0)
            {
                return null; // Нет доступных городов
            }

            // Выбираем случайный город из доступных
            int index = random.Next(availableCities.Count);
            string selectedCity = availableCities[index];

            // Добавляем выбранный город в список использованных
            AddCity(selectedCity);

            return selectedCity;
        }

        // Добавление города в список использованных.
        public void AddCity(string city)
        {
            UsedCities.Add(city);
        }

        // Очистка списка использованных городов.
        public void ClearUsedCities()
        {
            UsedCities.Clear();
        }

        // Добавление использованного города.
        public void AddUsedCity(string cityName)
        {
            UsedCities.Add(cityName);
        }
    }

}
