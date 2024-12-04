using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityMaster
{
    // Перечисление для выбора, кто ходит первым
    public enum StartingPlayer
    {
        Player,
        Computer
    }

    // Перечисление для выбора ограничения по времени
    public enum TimeLimit
    {
        Rush,       // 15 секунд
        Normal,     // 60 секунд
        Unlimited   // Без ограничений
    }

    public class GameSettings
    {
        // Свойства настроек игры
        public StartingPlayer WhoStartsFirst { get; set; }
        public TimeLimit SelectedTimeLimit { get; set; }
        public int NumberOfHints { get; set; }

        public GameSettings()
        {
            // Установка значений по умолчанию
            WhoStartsFirst = StartingPlayer.Player;
            SelectedTimeLimit = TimeLimit.Normal;
            NumberOfHints = 0;
        }
    }

}
