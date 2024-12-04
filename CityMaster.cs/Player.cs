using System.Collections.Generic;

namespace CityMaster
{
    public class Player
    {
        // Свойства
        public string Name { get; private set; }
        public List<string> UsedCities { get; private set; }
        public bool IsTurn { get; set; } // Отслеживание хода

        public Player(string name)
        {
            Name = name;
            UsedCities = new List<string>();
            IsTurn = true; // По умолчанию ходит игрок
        }

        // Добавление города в список использованных
        public void AddCity(string city)
        {
            UsedCities.Add(city);
        }

        // Очистка списка использованных городов
        public void ClearUsedCities()
        {
            UsedCities.Clear();
        }
    }

}
