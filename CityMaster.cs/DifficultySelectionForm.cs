using System;
using System.IO;
using System.Windows.Forms;

namespace CityMaster
{
    public partial class DifficultySelectionForm : Form
    {
        public DifficultySelectionForm()
        {
            InitializeComponent();

            // Установка дефолтного значения для ComboBox
            comboBoxHints.SelectedIndex = 1; // По умолчанию "1 штука"

            // Подписка на событие закрытия формы
            this.FormClosing += DifficultySelectionForm_FormClosing;
        }

        // Обработчик кнопки "Начать игру"
        private void buttonStartGame_Click(object sender, EventArgs e)
        {
            // Определение выбранных параметров
            bool computerFirst = radioButtonComputer.Checked;
            string selectedTimeLimit = "Normal"; // По умолчанию

            if (radioButtonRush.Checked)
                selectedTimeLimit = "Rush";
            else if (radioButtonNormal.Checked)
                selectedTimeLimit = "Normal";
            else if (radioButtonUnlimited.Checked)
                selectedTimeLimit = "NoLimit";

            // Получение количества подсказок из ComboBox
            int allowedHints = 0; // По умолчанию "Не использовать"

            switch (comboBoxHints.SelectedItem.ToString())
            {
                case "Не использовать":
                    allowedHints = 0;
                    break;
                case "1 штука":
                    allowedHints = 1;
                    break;
                case "2 штуки":
                    allowedHints = 2;
                    break;
                case "3 штуки":
                    allowedHints = 3;
                    break;
                case "Неограниченное":
                    allowedHints = -1; // -1 для неограниченных подсказок
                    break;
                default:
                    allowedHints = 1; // Дефолтное значение
                    break;
            }

            // Создание и показ формы игры
            City gameForm = new City(this, computerFirst, selectedTimeLimit, allowedHints);
            gameForm.Show();

            // Скрытие формы выбора сложности
            this.Hide();
        }

        // Обработчик кнопки "Покинуть игру"
        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Обработчик закрытия формы
        private void DifficultySelectionForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        // Метод для логирования ошибок в файл
        private void LogError(Exception ex)
        {
            string logFilePath = "ErrorLog.txt";
            string errorMessage = $"[{DateTime.Now}] {ex}\n";
            try
            {
                File.AppendAllText(logFilePath, errorMessage);
            }
            catch
            {
                // Если не удаётся записать в лог, ничего не делаем
            }
        }
    }
}
