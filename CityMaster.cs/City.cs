using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Linq;
using System.Drawing;
using System.Drawing.Printing;

namespace CityMaster
{
     public partial class City : Form
    {
        // Поля
        private Game game;
        private DifficultySelectionForm difficultyForm;
        private bool isComputerFirst;
        private string timeLimit;
        private int hintsAllowed;
        private int hintsUsed;

        // Поля для подсказок
        private bool hintUsedThisTurn;
        private string lastHintProvided;
        private bool allHintsUsedLogged;

        // Переменные для отслеживания времени
        private int remainingTime;
        private int totalTime;

        // Переменная для хранения текущей требуемой буквы
        private char? currentRequiredLetter;

        // Переменная для хранения первого города компьютера
        private string firstComputerCity;

        // Флаги
        private bool isFormClosing = false; 
        private bool isSurrendering = false;
        private bool isProtocolSaved = false; 

        // Конструкторы
        public City()
        {
            InitializeComponent();
            // Инициализация полей по умолчанию
            difficultyForm = null;
            isComputerFirst = false;
            timeLimit = "Normal";
            hintsAllowed = 1;
            hintsUsed = 0;
            hintUsedThisTurn = false;
            lastHintProvided = string.Empty;
            remainingTime = 0;
            totalTime = 0;
            allHintsUsedLogged = false;
            currentRequiredLetter = null;
            textBoxPlayer.Focus(); 
            this.AcceptButton = buttonConfirm; 
        }

        // Параметрический конструктор
        public City(DifficultySelectionForm parentForm, bool computerFirst, string selectedTimeLimit, int allowedHints)
        {
            InitializeComponent();
            difficultyForm = parentForm;
            isComputerFirst = computerFirst;
            timeLimit = selectedTimeLimit;
            hintsAllowed = allowedHints; 
            hintsUsed = 0;
            hintUsedThisTurn = false;
            lastHintProvided = string.Empty;
            remainingTime = 0;
            totalTime = 0;
            allHintsUsedLogged = false;
            currentRequiredLetter = null;
            textBoxPlayer.Focus(); 
            this.AcceptButton = buttonConfirm; 

            // Инициализация интерфейса подсказок
            UpdateHintsLabel();

            // Настройка таймера
            gameTimer.Tick += new EventHandler(gameTimer_Tick);

            // Инициализация игры с учётом выбранных параметров
            game = new Game("Игрок", "GameLog.log");
            game.InitializeGame();

            // Подписка на событие обновления лога после инициализации игры
            game.Logger.LogUpdated += Logger_LogUpdated;

            // Инициализация метки таймера
            if (timeLimit == "NoLimit")
            {
                labelTimer.Text = "Оставшееся время: ∞";
                gameTimer.Stop(); // Таймер не запускается для неограниченного режима
            }
            else
            {
                labelTimer.Text = "Оставшееся время: -- сек";
            }

            // Первоначальный ход
            if (isComputerFirst)
            {
                ComputerFirstMove();
            }
            else
            {
                StartPlayerTurnTimer();
            }

            // Обновление лога сразу после инициализации
            UpdateGameLog();
        }

        // Обработчики событий
        private void Logger_LogUpdated(object sender, EventArgs e)
        {
            UpdateGameLog();
        }

        private void City_Load(object sender, EventArgs e)
        {
            // Метод вызывается при загрузке формы
            UpdateHintsLabel();
            ResetGameTimer();
            textBoxPlayer.Focus(); 

            // Обновляем лог при загрузке
            UpdateGameLog();
        }

        private void textBoxPlayer_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Разрешаем только русские буквы, тире, пробел и Backspace
            if (!IsAllowedChar(e.KeyChar))
            {
                e.Handled = true; 
            }
        }

        private bool IsAllowedChar(char c)
        {
            return (char.IsControl(c) && c == '\b') || 
                   IsRussianLetter(c) ||
                   c == '-' ||
                   c == ' ';
        }

        private void City_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isFormClosing)
                return;

            isFormClosing = true;

            // Останавливаем таймер
            if (gameTimer.Enabled)
                gameTimer.Stop();

            // Проверка завершена ли игра
            if (!game.IsGameOver())
            {
                // Подтверждение выхода из игры
                DialogResult confirmEndGame = MessageBox.Show(
                    "Вы уверены, что хотите выйти из игры? Вам будет присуждено поражение.",
                    "Подтверждение выхода",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (confirmEndGame == DialogResult.Yes)
                {
                    // Логируем досрочное завершение игры
                    game.GameOverAsDefeat();
                    game.Logger.LogEvent("Пользователь завершил игру досрочно (закрыл окно) и проиграл.");

                    // Предлагаем сохранить или распечатать протокол
                    OfferSaveOrPrintProtocol();

                    // Возвращаемся в меню выбора сложности
                    Application.Exit();
                }
                else
                {
                    // Отменяем закрытие формы
                    e.Cancel = true;
                    isFormClosing = false;
                }
            }
            else
            {
                // Закрытие формы, если игра уже завершена
                this.Close();
                difficultyForm?.Show();
            }
        }

        // Методы
        private void UpdateGameLog()
        {
            // Очищаем richTextBoxGameLog
            richTextBoxGameLog.Clear();

            // Получаем текущий протокол игры из логгера
            string[] logLines = game.Logger.GetCurrentGameLog().Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            foreach (string line in logLines)
            {
                if (line.Contains("Игрок"))
                {
                    // Ходы игрока — синим цветом
                    richTextBoxGameLog.SelectionColor = Color.Blue;
                }
                else if (line.Contains("Компьютер"))
                {
                    // Ходы компьютера — красным цветом
                    richTextBoxGameLog.SelectionColor = Color.Red;
                }
                else if (line.Contains("Все подсказки использованы.") || line.Contains("Подсказки закончились"))
                {
                    // Информация о том, что подсказки закончились — оранжевым цветом
                    richTextBoxGameLog.SelectionColor = Color.Orange;
                }
                else
                {
                    // Остальной текст — чёрным цветом
                    richTextBoxGameLog.SelectionColor = Color.Black;
                }

                richTextBoxGameLog.AppendText(line + Environment.NewLine);
            }

            // Прокручиваем вниз
            richTextBoxGameLog.SelectionStart = richTextBoxGameLog.Text.Length;
            richTextBoxGameLog.ScrollToCaret();
        }

        private bool IsRussianLetter(char c)
        {
            return (c >= 'А' && c <= 'Я') ||
                   (c >= 'а' && c <= 'я') ||
                   c == 'Ё' || c == 'ё';
        }

        private char GetRequiredLetter(string cityName)
        {
            char lastLetter = game.GetLastValidLetter(cityName);
            if (lastLetter == 'ь' || lastLetter == 'ъ' || lastLetter == 'ы')
            {
                if (cityName.Length >= 2)
                {
                    lastLetter = char.ToLower(cityName[cityName.Length - 2]);
                }
                else
                {
                    lastLetter = ' ';
                }
            }
            else
            {
                lastLetter = char.ToLower(lastLetter);
            }
            return lastLetter;
        }

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            string userCity = textBoxPlayer.Text.Trim();

            try
            {
                if (string.IsNullOrEmpty(userCity))
                {
                    MessageBox.Show("Введите название города.", "Неверный ввод", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBoxPlayer.Focus();
                    return;
                }

                // Проверка, начинается ли название города с заглавной буквы
                if (!char.IsUpper(userCity[0]))
                {
                    MessageBox.Show("Название города должно начинаться с заглавной буквы.", "Неверный ввод", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBoxPlayer.Focus();
                    return;
                }

                if (currentRequiredLetter.HasValue)
                {
                    char requiredFirstLetter = char.ToLower(currentRequiredLetter.Value);
                    if (char.ToLower(userCity[0]) != requiredFirstLetter)
                    {
                        MessageBox.Show($"Город должен начинаться с буквы '{char.ToUpper(requiredFirstLetter)}'.", "Неверный ввод", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        textBoxPlayer.Focus();
                        return;
                    }
                }

                string computerCity = game.PlayerMove(userCity);

                textBoxComputer.Text = computerCity;

                if (game.IsGameOver())
                {
                    string gameResult = game.GetGameResult();

                    bool userWon = gameResult.IndexOf("победил компьютер", StringComparison.OrdinalIgnoreCase) == -1;

                    if (userWon)
                    {
                        MessageBox.Show("Поздравляем! Вы победили! Игра завершена.", "Победа", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Вы проиграли. Игра завершена.", "Поражение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    // Предлагаем сохранить или распечатать протокол
                    OfferSaveOrPrintProtocol();

                    // Возвращаемся в меню выбора сложности
                    EndGame();
                }
                else
                {
                    char lastLetter = GetRequiredLetter(computerCity);

                    if (lastLetter != ' ')
                    {
                        labelNextLetter.Text = $"(Необходимо начать с буквы - {char.ToUpper(lastLetter)})";
                        currentRequiredLetter = char.ToLower(lastLetter);
                    }
                    else
                    {
                        labelNextLetter.Text = "(Необходимо начать с буквы - …)";
                        currentRequiredLetter = null;
                    }

                    StartPlayerTurnTimer();
                    ResetGameTimer();

                    hintUsedThisTurn = false;
                    lastHintProvided = string.Empty;
                }

                textBoxPlayer.Text = "";
                textBoxPlayer.Focus();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Неверный ввод", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxPlayer.Focus();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ResetGameInterface();
                textBoxPlayer.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Неизвестная ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxPlayer.Focus();
            }
        }

        private void buttonHint_Click(object sender, EventArgs e)
        {
            if (game.IsGameOver())
            {
                MessageBox.Show("Игра уже завершена.", "Подсказка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (hintUsedThisTurn)
            {
                if (!string.IsNullOrEmpty(lastHintProvided))
                {
                    MessageBox.Show($"Вы уже получили подсказку в этом ходу: \"{lastHintProvided}\".", "Подсказка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBoxPlayer.Focus();
                }
                else
                {
                    MessageBox.Show("Вы уже использовали подсказку в этом ходу.", "Подсказка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBoxPlayer.Focus();
                }
                return;
            }

            if (hintsAllowed != -1 && hintsUsed >= hintsAllowed)
            {
                // Добавляем информацию о том, что подсказки закончились, в richTextBoxGameLog оранжевым цветом
                richTextBoxGameLog.SelectionColor = Color.Orange;
                richTextBoxGameLog.AppendText("Подсказки закончились." + Environment.NewLine);
                richTextBoxGameLog.SelectionColor = Color.Black; 

                MessageBox.Show("У вас больше нет подсказок.", "Подсказка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxPlayer.Focus();
                return;
            }

            if (!currentRequiredLetter.HasValue)
            {
                MessageBox.Show("Компьютер ещё не ввёл город, чтобы давать подсказку", "Подсказка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string hintCity = game.GetHint(currentRequiredLetter.Value);

            if (!string.IsNullOrEmpty(hintCity))
            {
                // Сначала логируем использование подсказки
                if (hintsAllowed == -1)
                {
                    game.Logger.LogEvent("Пользователь использовал подсказку.");
                }
                else if (hintsAllowed > 0)
                {
                    hintsUsed++;
                    game.Logger.LogEvent($"Пользователь использовал подсказку. Осталось подсказок: {hintsAllowed - hintsUsed}");

                    if (hintsUsed >= hintsAllowed && !allHintsUsedLogged)
                    {
                        game.Logger.LogEvent("Все подсказки использованы.");
                        allHintsUsedLogged = true;
                    }
                }

                // Затем показываем подсказку пользователю
                MessageBox.Show($"Подсказка: {hintCity}", "Подсказка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBoxPlayer.Focus();

                hintUsedThisTurn = true;
                lastHintProvided = hintCity;
                UpdateHintsLabel();
            }
            else
            {
                MessageBox.Show("Нет доступных подсказок.", "Подсказка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBoxPlayer.Focus();
            }
        }

        private void buttonSurrender_Click(object sender, EventArgs e) // "Сдаться"
        {
            if (game != null && !game.IsGameOver())
            {
                // Подтверждение сдачи
                DialogResult confirmSurrender = MessageBox.Show(
                    "Вы уверены, что хотите сдаться? Вам будет присуждено поражение.",
                    "Подтверждение сдачи",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (confirmSurrender == DialogResult.Yes)
                {
                    // Устанавливаем флаг сдачи
                    isSurrendering = true;

                    // Останавливаем таймер
                    if (gameTimer.Enabled)
                    {
                        gameTimer.Stop();
                    }

                    // Пользователь подтвердил сдачу
                    game.Surrender();

                    // Логируем поражение
                    game.GameOverAsDefeat();
                    game.Logger.LogEvent("Пользователь сдался и проиграл.");

                    // Предлагаем сохранить или распечатать протокол
                    OfferSaveOrPrintProtocol();

                    // Возвращаем пользователя в окно выбора сложности
                    EndGame();
                }
            }
        }

        private void buttonExitGame_Click(object sender, EventArgs e)
        {
            // Подтверждение выхода из игры
            DialogResult confirmExit = MessageBox.Show(
                "Вы уверены, что хотите покинуть игру? Вам будет присуждено поражение.",
                "Подтверждение выхода",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirmExit == DialogResult.Yes)
            {
                // Логируем поражение
                game.GameOverAsDefeat();
                game.Logger.LogEvent("Пользователь покинул игру через кнопку 'Покинуть игру' и проиграл.");

                // Предлагаем сохранить или распечатать протокол
                OfferSaveOrPrintProtocol();

                this.Close();
                Application.Exit();
            }
        }

        private void ComputerFirstMove()
        {
            try
            {
                List<string> allCities = game.CityManager.GetAllCities();

                List<string> availableCities = allCities
                    .Except(game.player.UsedCities, StringComparer.OrdinalIgnoreCase)
                    .Except(game.computer.UsedCities, StringComparer.OrdinalIgnoreCase)
                    .ToList();

                if (availableCities.Count == 0)
                {
                    MessageBox.Show("Нет доступных городов для начала игры.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    EndGame();
                    return;
                }

                Random rnd = new Random();
                int index = rnd.Next(availableCities.Count);
                firstComputerCity = availableCities[index];

                game.Logger.LogEvent($"Компьютер начинает игру с города: {firstComputerCity}");

                game.computer.AddUsedCity(firstComputerCity);

                textBoxComputer.Text = firstComputerCity;

                char lastLetter = GetRequiredLetter(firstComputerCity);

                if (lastLetter != ' ')
                {
                    labelNextLetter.Text = $"(Необходимо начать с буквы - {char.ToUpper(lastLetter)})";
                    currentRequiredLetter = char.ToLower(lastLetter);
                }
                else
                {
                    labelNextLetter.Text = "(Необходимо начать с буквы - …)";
                    currentRequiredLetter = null;
                }

                StartPlayerTurnTimer();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при ходе компьютера: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                EndGame();
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            if (remainingTime > 0)
            {
                remainingTime--;
                labelTimer.Text = $"Оставшееся время: {remainingTime} сек";

                if (remainingTime == 0)
                {
                    gameTimer.Stop();
                    MessageBox.Show("Время вышло! Вы проиграли.", "Время вышло", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Логируем поражение
                    game.GameOverAsDefeat();
                    game.Logger.LogEvent("Время вышло. Пользователь проиграл.");

                    // Предлагаем сохранить или распечатать протокол
                    OfferSaveOrPrintProtocol();

                    // Завершаем игру
                    EndGame();
                }
            }
        }

        private void StartPlayerTurnTimer()
        {
            // Останавливаем таймер перед настройкой
            gameTimer.Stop();

            switch (timeLimit)
            {
                case "Rush":
                    remainingTime = 15; 
                    totalTime = 15;
                    break;
                case "Normal":
                    remainingTime = 60; 
                    totalTime = 60;
                    break;
                case "NoLimit":
                    remainingTime = -1; 
                    totalTime = -1;
                    break;
                default:
                    remainingTime = 60;
                    totalTime = 60;
                    break;
            }

            if (remainingTime > 0)
            {
                labelTimer.Text = $"Оставшееся время: {remainingTime} сек";
                gameTimer.Interval = 1000; // 1 секунда
                gameTimer.Start();
            }
            else if (timeLimit == "NoLimit")
            {
                labelTimer.Text = "Оставшееся время: ∞";
                gameTimer.Stop();
            }
            else
            {
                labelTimer.Text = "Оставшееся время: Нет ограничения";
                gameTimer.Stop();
            }
        }

        private void EndGame()
        {
            // Останавливаем таймер, если он еще запущен
            if (gameTimer.Enabled)
                gameTimer.Stop();

            // Сбрасываем метку времени
            labelTimer.Text = "Оставшееся время: -- сек";


            // Закрываем форму и возвращаемся к форме выбора сложности
            this.Close();
            difficultyForm?.Show();
        }

        private void ResetGameTimer()
        {
            if (timeLimit == "NoLimit")
            {
                labelTimer.Text = "Оставшееся время: ∞";
                gameTimer.Stop();
            }
            else
            {
                switch (timeLimit)
                {
                    case "Rush":
                        remainingTime = 15; 
                        break;
                    case "Normal":
                        remainingTime = 60; 
                        break;
                    default:
                        remainingTime = 60; 
                        break;
                }

                labelTimer.Text = $"Оставшееся время: {remainingTime} сек";
                gameTimer.Interval = 1000;
                gameTimer.Start();
            }
        }

        private void ResetGameInterface()
        {
            labelNextLetter.Text = "(Необходимо начать с буквы - …)";
            textBoxComputer.Text = "";
            textBoxPlayer.Text = "";
            hintsUsed = 0;
            hintUsedThisTurn = false;
            lastHintProvided = string.Empty;
            UpdateHintsLabel();
            labelTimer.Text = "Оставшееся время: -- сек";
            allHintsUsedLogged = false;
            currentRequiredLetter = null;
        }

        private void SaveGameProtocol()
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Текстовые файлы (*.log)|*.log|Все файлы (*.*)|*.*";
                saveFileDialog.Title = "Сохранить протокол игры";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Получаем текст текущего протокола игры из логгера
                        string gameLogText = game.Logger.GetCurrentGameLog();

                        // Записываем его в выбранный файл
                        File.WriteAllText(saveFileDialog.FileName, gameLogText, Encoding.UTF8);
                        MessageBox.Show("Протокол игры успешно сохранён.", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Устанавливаем флаг, что протокол сохранён
                        isProtocolSaved = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при сохранении протокола: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void PrintGameProtocol()
        {
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += PrintDocument_PrintPage;

            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument;

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    printDocument.Print();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при печати: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            // Получаем текст текущего протокола игры
            string gameLogText = game.Logger.GetCurrentGameLog();

            // Устанавливаем шрифт и параметры
            Font printFont = new Font("Times New Roman", 14);
            float leftMargin = e.MarginBounds.Left;
            float topMargin = e.MarginBounds.Top;
            float maxWidth = e.MarginBounds.Width;
            float maxHeight = e.MarginBounds.Height;

            // Разбиваем текст на строки с учетом ширины страницы
            string[] lines = SplitTextIntoLines(gameLogText, printFont, e.Graphics, maxWidth);

            // Вычисляем позицию для печати каждой строки
            float lineHeight = printFont.GetHeight(e.Graphics);
            float yPosition = topMargin;
            int lineIndex = 0;

            while (lineIndex < lines.Length)
            {
                e.Graphics.DrawString(lines[lineIndex], printFont, Brushes.Black, leftMargin, yPosition);
                yPosition += lineHeight;
                lineIndex++;

                // Проверяем, не выходим ли за пределы страницы
                if (yPosition + lineHeight > topMargin + maxHeight)
                {
                    e.HasMorePages = true;
                    return;
                }
            }

            e.HasMorePages = false;
        }

        private string[] SplitTextIntoLines(string text, Font font, Graphics graphics, float maxWidth)
        {
            List<string> lines = new List<string>();
            string[] paragraphs = text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            foreach (string paragraph in paragraphs)
            {
                string remainingText = paragraph;
                while (!string.IsNullOrEmpty(remainingText))
                {
                    int charFit = 0;

                    // Определяем, сколько символов поместится в одной строке
                    SizeF size = graphics.MeasureString(remainingText, font, new SizeF(maxWidth, 0), StringFormat.GenericDefault);
                    charFit = (int)(maxWidth / graphics.MeasureString("A", font).Width * remainingText.Length / 2);
                    if (charFit <= 0) charFit = 1;

                    // Добавляем строку
                    if (charFit > 0)
                    {
                        int length = Math.Min(charFit, remainingText.Length);
                        lines.Add(remainingText.Substring(0, length).TrimEnd());

                        // Обрезаем обработанную часть текста
                        remainingText = remainingText.Substring(length).TrimStart();
                    }
                    else
                    {
                        // Если charFit == 0, чтобы избежать бесконечного цикла
                        break;
                    }
                }
            }

            return lines.ToArray();
        }

        private void OfferSaveOrPrintProtocol()
        {
            // Предлагаем сохранить протокол игры
            DialogResult saveOption = MessageBox.Show(
                "Вы хотите сохранить протокол игры?",
                "Сохранение протокола",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (saveOption == DialogResult.Yes)
            {
                // Спрашиваем, хочет ли пользователь сохранить протокол в файл .log
                DialogResult saveToFile = MessageBox.Show(
                    "Вы хотите сохранить протокол в файл .log?",
                    "Сохранение протокола",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (saveToFile == DialogResult.Yes)
                {
                    SaveGameProtocol();
                }

                // Независимо от выбора сохранения, спрашиваем о печати протокола
                DialogResult printOption = MessageBox.Show(
                    "Вы хотите распечатать протокол игры?",
                    "Печать протокола",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (printOption == DialogResult.Yes)
                {
                    PrintGameProtocol();
                }
            }
        }

        // Метод для обновления метки подсказок
        private void UpdateHintsLabel()
        {
            if (hintsAllowed == -1)
            {
                labelHintsRemaining.Text = "Количество подсказок: ∞";
                labelHintsRemaining.ForeColor = Color.Purple;
            }
            else if (hintsAllowed > 0)
            {
                int remainingHints = hintsAllowed - hintsUsed;
                labelHintsRemaining.Text = $"Осталось подсказок: {remainingHints}";
            }
            else
            {
                labelHintsRemaining.Text = $"Осталось подсказок: 0";
            }
        }
    }
}
