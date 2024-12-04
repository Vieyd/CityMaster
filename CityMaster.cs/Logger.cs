using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace CityMaster
{
    public class Logger
    {
        private string logFilePath;
        private int currentGameNumber;
        private StringBuilder currentGameLog;

        // Событие обновления лога
        public event EventHandler LogUpdated;

        public Logger(string filePath)
        {
            logFilePath = filePath;
            currentGameLog = new StringBuilder();

            // Определение последнего номера игры
            if (File.Exists(logFilePath))
            {
                string[] lines = File.ReadAllLines(logFilePath);
                currentGameNumber = 0;
                foreach (string line in lines)
                {
                    // Ищем строки, начинающиеся с "Игра №X"
                    Match match = Regex.Match(line, @"^Игра №(\d+)", RegexOptions.IgnoreCase);
                    if (match.Success)
                    {
                        if (int.TryParse(match.Groups[1].Value, out int gameNumber))
                        {
                            if (gameNumber > currentGameNumber)
                                currentGameNumber = gameNumber;
                        }
                    }
                }
            }
            else
            {
                // Создание файла лога, если он не существует
                File.WriteAllText(logFilePath, "Протокол игры:\n");
                currentGameNumber = 0;
            }
        }

        // Начало новой игры
        public void StartNewGame()
        {
            currentGameNumber++;
            string header = $"Игра №{currentGameNumber}\nНачало игры: {DateTime.Now}";
            File.AppendAllText(logFilePath, header + "\n");
            currentGameLog.Clear();
            currentGameLog.AppendLine(header);

            // Вызов события обновления лога
            OnLogUpdated();
        }

        // Логирование хода
        public void LogMove(string player, string city)
        {
            string logEntry = $"{DateTime.Now}: {player} назвал город {city}";
            File.AppendAllText(logFilePath, logEntry + "\n");
            currentGameLog.AppendLine(logEntry);

            // Вызов события обновления лога
            OnLogUpdated();
        }

        // Логирование произвольного события
        public void LogEvent(string eventDescription)
        {
            string logEntry = $"{DateTime.Now}: {eventDescription}";
            File.AppendAllText(logFilePath, logEntry + "\n");
            currentGameLog.AppendLine(logEntry);

            // Вызов события обновления лога
            OnLogUpdated();
        }

        // Завершение записи (для итогов игры)
        public void SaveLog(string result)
        {
            string logEntry = $"{DateTime.Now}: {result}";
            File.AppendAllText(logFilePath, logEntry + "\n");
            currentGameLog.AppendLine(logEntry);

            // Вызов события обновления лога
            OnLogUpdated();
        }

        // Получение текущего игрового лога
        public string GetCurrentGameLog()
        {
            return currentGameLog.ToString();
        }

        // Очистка лога (не используется)
        public void ClearLog()
        {
            // Оставляем заголовок протокола
            File.WriteAllText(logFilePath, "Протокол игры:\n");
            currentGameNumber = 0;
            currentGameLog.Clear();

            // Вызов события обновления лога
            OnLogUpdated();
        }

        // Вызов события обновления лога
        protected virtual void OnLogUpdated()
        {
            LogUpdated?.Invoke(this, EventArgs.Empty);
        }
    }

}
