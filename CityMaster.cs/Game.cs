using System;
using System.Collections.Generic;
using System.Linq;

namespace CityMaster
{
    public class Game
    {
        // Свойства
        public Player player { get; private set; }
        public ComputerPlayer computer { get; private set; }
        public Logger Logger { get; private set; }
        public CityManager CityManager { get; private set; }

        // Поля
        private char lastLetter;
        private bool isGameOver;
        private string gameResult; // Для хранения результата игры

        // Конструктор
        public Game(string playerName, string logFilePath)
        {
            player = new Player(playerName);
            computer = new ComputerPlayer("Компьютер");
            Logger = new Logger(logFilePath);
            CityManager = new CityManager();
            lastLetter = ' '; // Изначально нет последней буквы
            isGameOver = false;
            gameResult = string.Empty;
        }

        // Внутренний метод для получения последней допустимой буквы
        private char GetLastValidLetterInternal(string city)
        {
            // Список недопустимых букв
            HashSet<char> invalidLetters = new HashSet<char> { 'ь', 'ъ', 'ы' };

            // Проходимся с конца названия города
            for (int i = city.Length - 1; i >= 0; i--)
            {
                char currentChar = char.ToLower(city[i]);
                if (!invalidLetters.Contains(currentChar))
                {
                    return currentChar;
                }
            }

            // Если все буквы недопустимы, возвращаем пробел
            return ' ';
        }

        // Публичный метод для получения последней допустимой буквы
        public char GetLastValidLetter(string city)
        {
            return GetLastValidLetterInternal(city);
        }

        // Инициализация игры
        public void InitializeGame()
        {
            // Очистка использованных городов
            player.ClearUsedCities();
            computer.ClearUsedCities();

            lastLetter = ' ';
            isGameOver = false;
            gameResult = string.Empty;

            // Логируем начало новой игры
            Logger.StartNewGame();
            Logger.LogEvent("Началась новая игра.");

            // Установка хода игрока
            player.IsTurn = true;
            computer.IsTurn = false;
        }

        // Ход игрока или компьютера
        public string PlayerMove(string city)
        {
            if (isGameOver)
                throw new InvalidOperationException("Игра уже завершена.");

            city = city.Trim();

            if (string.IsNullOrEmpty(city))
                throw new ArgumentException("Город не может быть пустым.");

            if (!CityManager.ContainsCity(city))
            {
                double threshold = Math.Log(city.Length);
                string suggestedCity = CityManager.GetAllCities()
                    .FirstOrDefault(c => CalculateLevenshteinDistance(city.ToLower(), c.ToLower()) <= threshold);

                if (!string.IsNullOrEmpty(suggestedCity))
                {
                    throw new ArgumentException($"Такого города не существует. Вы имели в виду: {suggestedCity}?");
                }
                else
                {
                    throw new ArgumentException("Такого города не существует.");
                }
            }

            // Проверка на повторное использование города
            if (player.UsedCities.Any(c => string.Equals(c, city, StringComparison.OrdinalIgnoreCase)) ||
                computer.UsedCities.Any(c => string.Equals(c, city, StringComparison.OrdinalIgnoreCase)))
                throw new ArgumentException("Этот город уже был использован в игре.");

            // Если это не первый ход, проверяем первую букву
            if (lastLetter != ' ' && char.ToLower(city[0]) != lastLetter)
                throw new ArgumentException($"Город должен начинаться с буквы '{char.ToUpper(lastLetter)}'.");

            // Добавление города
            if (player.IsTurn)
            {
                player.AddCity(city);
                Logger.LogMove(player.Name, city);
            }
            else
            {
                computer.AddCity(city);
                Logger.LogMove(computer.Name, city);
            }

            // Получаем последнюю допустимую букву
            lastLetter = GetLastValidLetterInternal(city);



            // Проверка, закончилась ли игра из-за недопустимой буквы
            if (lastLetter == ' ')
            {
                isGameOver = true;
                if (player.IsTurn)
                {
                    gameResult = $"{player.Name} назвал город, заканчивающийся на недопустимую букву. Победил компьютер.";
                }
                else
                {
                    gameResult = $"{computer.Name} назвал город, заканчивающийся на недопустимую букву. Победил игрок.";
                }
                Logger.SaveLog(gameResult);
                return $"Игра завершена. {gameResult}";
            }

            // Меняем ход
            if (player.IsTurn)
            {
                player.IsTurn = false;
                computer.IsTurn = true;
                return ComputerMove();
            }
            else
            {
                player.IsTurn = true;
                computer.IsTurn = false;
                return null; // Ход игрока
            }
        }

        // Функция для вычисления расстояния Левенштейна
        private int CalculateLevenshteinDistance(string source, string target)
        {
            int[,] dp = new int[source.Length + 1, target.Length + 1];
            for (int i = 0; i <= source.Length; i++) dp[i, 0] = i;
            for (int j = 0; j <= target.Length; j++) dp[0, j] = j;

            for (int i = 1; i <= source.Length; i++)
            {
                for (int j = 1; j <= target.Length; j++)
                {
                    int cost = (source[i - 1] == target[j - 1]) ? 0 : 1;
                    dp[i, j] = Math.Min(
                        Math.Min(dp[i - 1, j] + 1, dp[i, j - 1] + 1),
                        dp[i - 1, j - 1] + cost
                    );
                }
            }

            return dp[source.Length, target.Length];
        }


        // Ход компьютера
        private string ComputerMove()
        {
            // Получение доступных городов для компьютера
            HashSet<string> excludeCities = new HashSet<string>(player.UsedCities, StringComparer.OrdinalIgnoreCase);
            excludeCities.UnionWith(computer.UsedCities);

            List<string> availableCities = CityManager.GetCitiesStartingWith(lastLetter, excludeCities);

            if (availableCities.Count == 0)
            {
                isGameOver = true;
                gameResult = $"{player.Name} победил! Компьютер не может назвать город.";
                Logger.SaveLog(gameResult);
                return $"Вы победили! Компьютер не может назвать город.";
            }

            // Компьютер выбирает город
            string computerCity = computer.MakeMove(lastLetter, availableCities);
            Logger.LogMove(computer.Name, computerCity);

            // Получаем последнюю допустимую букву
            lastLetter = GetLastValidLetter(computerCity);


            // Меняем ход
            player.IsTurn = true;
            computer.IsTurn = false;

            return computerCity;
        }

        // Метод для получения подсказки
        public string GetHint(char requiredLetter)
        {
            // Получение доступных городов для подсказки
            var availableHints = CityManager.GetAllCities()
                .Where(city => char.ToLower(city[0]) == requiredLetter &&
                               !player.UsedCities.Any(c => string.Equals(c, city, StringComparison.OrdinalIgnoreCase)) &&
                               !computer.UsedCities.Any(c => string.Equals(c, city, StringComparison.OrdinalIgnoreCase)))
                .ToList();

            if (availableHints.Count == 0)
                return null; // Нет доступных подсказок

            // Выбор случайного города из подсказок
            Random rnd = new Random();
            int index = rnd.Next(availableHints.Count);
            string hintCity = availableHints[index];

            // Логируем выбор подсказки
            LogEvent($"Подсказка: {hintCity}");

            return hintCity;
        }

        // Сдача игрока
        public void Surrender()
        {
            if (isGameOver)
                throw new InvalidOperationException("Игра уже завершена.");

            isGameOver = true;
            gameResult = $"{player.Name} сдался. Победил {computer.Name}.";
            Logger.SaveLog(gameResult);
        }

        // Завершение игры как поражение пользователя
        public void GameOverAsDefeat()
        {
            if (!isGameOver)
            {
                isGameOver = true;
                gameResult = $"{player.Name} начал новую игру и проиграл текущую.";
                Logger.SaveLog(gameResult);
            }
        }

        // Проверка завершена ли игра
        public bool IsGameOver()
        {
            return isGameOver;
        }

        // Логирование событий
        public void LogEvent(string eventDescription)
        {
            Logger.LogEvent(eventDescription);
        }

        // Получение результата игры
        public string GetGameResult()
        {
            return gameResult;
        }
    }
}
