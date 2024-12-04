using System;
using System.Windows.Forms;

namespace CityMaster
{
    public partial class WelcomeForm : Form
    {
        public WelcomeForm()
        {
            InitializeComponent();
            this.FormClosing += WelcomeForm_FormClosing; // Подписка на событие FormClosing
        }

        // Обработчик кнопки "Начать игру"
        private void buttonStart_Click(object sender, EventArgs e)
        {
            // Создание и показ формы выбора сложности
            DifficultySelectionForm difficultyForm = new DifficultySelectionForm();
            difficultyForm.Show();

            // Скрытие формы приветствия
            this.Hide();
        }

        // Обработчик кнопки "Покинуть игру"
        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Обработчик закрытия формы
        private void WelcomeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }

}
