namespace CityMaster
{
    partial class DifficultySelectionForm
    {
        private System.ComponentModel.IContainer components = null;

        // Освобождение ресурсов
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        // Метод инициализации компонентов формы
        private void InitializeComponent()
        {
            this.groupBoxStartingPlayer = new System.Windows.Forms.GroupBox();
            this.radioButtonComputer = new System.Windows.Forms.RadioButton();
            this.radioButtonPlayer = new System.Windows.Forms.RadioButton();
            this.groupBoxTimeLimit = new System.Windows.Forms.GroupBox();
            this.radioButtonUnlimited = new System.Windows.Forms.RadioButton();
            this.radioButtonNormal = new System.Windows.Forms.RadioButton();
            this.radioButtonRush = new System.Windows.Forms.RadioButton();
            this.labelHints = new System.Windows.Forms.Label();
            this.comboBoxHints = new System.Windows.Forms.ComboBox();
            this.buttonStartGame = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.groupBoxStartingPlayer.SuspendLayout();
            this.groupBoxTimeLimit.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxStartingPlayer
            // 
            this.groupBoxStartingPlayer.Controls.Add(this.radioButtonComputer);
            this.groupBoxStartingPlayer.Controls.Add(this.radioButtonPlayer);
            this.groupBoxStartingPlayer.Location = new System.Drawing.Point(50, 34);
            this.groupBoxStartingPlayer.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.groupBoxStartingPlayer.Name = "groupBoxStartingPlayer";
            this.groupBoxStartingPlayer.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.groupBoxStartingPlayer.Size = new System.Drawing.Size(300, 120);
            this.groupBoxStartingPlayer.TabIndex = 2;
            this.groupBoxStartingPlayer.TabStop = false;
            this.groupBoxStartingPlayer.Text = "Кто ходит первым";
            // 
            // radioButtonComputer
            // 
            this.radioButtonComputer.AutoSize = true;
            this.radioButtonComputer.Location = new System.Drawing.Point(32, 70);
            this.radioButtonComputer.Name = "radioButtonComputer";
            this.radioButtonComputer.Size = new System.Drawing.Size(145, 30);
            this.radioButtonComputer.TabIndex = 4;
            this.radioButtonComputer.TabStop = true;
            this.radioButtonComputer.Text = "Компьютер";
            this.radioButtonComputer.UseVisualStyleBackColor = true;
            // 
            // radioButtonPlayer
            // 
            this.radioButtonPlayer.AutoSize = true;
            this.radioButtonPlayer.Checked = true;
            this.radioButtonPlayer.Location = new System.Drawing.Point(32, 34);
            this.radioButtonPlayer.Name = "radioButtonPlayer";
            this.radioButtonPlayer.Size = new System.Drawing.Size(92, 30);
            this.radioButtonPlayer.TabIndex = 3;
            this.radioButtonPlayer.TabStop = true;
            this.radioButtonPlayer.Text = "Игрок";
            this.radioButtonPlayer.UseVisualStyleBackColor = true;
            // 
            // groupBoxTimeLimit
            // 
            this.groupBoxTimeLimit.Controls.Add(this.radioButtonUnlimited);
            this.groupBoxTimeLimit.Controls.Add(this.radioButtonNormal);
            this.groupBoxTimeLimit.Controls.Add(this.radioButtonRush);
            this.groupBoxTimeLimit.Location = new System.Drawing.Point(50, 184);
            this.groupBoxTimeLimit.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.groupBoxTimeLimit.Name = "groupBoxTimeLimit";
            this.groupBoxTimeLimit.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.groupBoxTimeLimit.Size = new System.Drawing.Size(300, 155);
            this.groupBoxTimeLimit.TabIndex = 5;
            this.groupBoxTimeLimit.TabStop = false;
            this.groupBoxTimeLimit.Text = "Выбор режима игры:";
            // 
            // radioButtonUnlimited
            // 
            this.radioButtonUnlimited.AutoSize = true;
            this.radioButtonUnlimited.Location = new System.Drawing.Point(30, 111);
            this.radioButtonUnlimited.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.radioButtonUnlimited.Name = "radioButtonUnlimited";
            this.radioButtonUnlimited.Size = new System.Drawing.Size(196, 30);
            this.radioButtonUnlimited.TabIndex = 8;
            this.radioButtonUnlimited.TabStop = true;
            this.radioButtonUnlimited.Text = "Без ограничений";
            this.radioButtonUnlimited.UseVisualStyleBackColor = true;
            // 
            // radioButtonNormal
            // 
            this.radioButtonNormal.AutoSize = true;
            this.radioButtonNormal.Checked = true;
            this.radioButtonNormal.Location = new System.Drawing.Point(30, 73);
            this.radioButtonNormal.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.radioButtonNormal.Name = "radioButtonNormal";
            this.radioButtonNormal.Size = new System.Drawing.Size(245, 30);
            this.radioButtonNormal.TabIndex = 7;
            this.radioButtonNormal.TabStop = true;
            this.radioButtonNormal.Text = "Обычный (60 секунд)";
            this.radioButtonNormal.UseVisualStyleBackColor = true;
            // 
            // radioButtonRush
            // 
            this.radioButtonRush.AutoSize = true;
            this.radioButtonRush.Location = new System.Drawing.Point(32, 35);
            this.radioButtonRush.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.radioButtonRush.Name = "radioButtonRush";
            this.radioButtonRush.Size = new System.Drawing.Size(237, 30);
            this.radioButtonRush.TabIndex = 6;
            this.radioButtonRush.Text = "Быстрый (15 секунд)";
            this.radioButtonRush.UseVisualStyleBackColor = true;
            // 
            // labelHints
            // 
            this.labelHints.AutoSize = true;
            this.labelHints.Location = new System.Drawing.Point(77, 364);
            this.labelHints.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.labelHints.Name = "labelHints";
            this.labelHints.Size = new System.Drawing.Size(237, 26);
            this.labelHints.TabIndex = 2;
            this.labelHints.Text = "Количество подсказок:";
            // 
            // comboBoxHints
            // 
            this.comboBoxHints.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxHints.FormattingEnabled = true;
            this.comboBoxHints.Items.AddRange(new object[] {
            "Не использовать",
            "1 штука",
            "2 штуки",
            "3 штуки",
            "Неограниченное"});
            this.comboBoxHints.Location = new System.Drawing.Point(82, 408);
            this.comboBoxHints.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.comboBoxHints.Name = "comboBoxHints";
            this.comboBoxHints.Size = new System.Drawing.Size(232, 34);
            this.comboBoxHints.TabIndex = 9;
            // 
            // buttonStartGame
            // 
            this.buttonStartGame.Location = new System.Drawing.Point(50, 478);
            this.buttonStartGame.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.buttonStartGame.Name = "buttonStartGame";
            this.buttonStartGame.Size = new System.Drawing.Size(300, 50);
            this.buttonStartGame.TabIndex = 0;
            this.buttonStartGame.Text = "Начать игру";
            this.buttonStartGame.UseVisualStyleBackColor = true;
            this.buttonStartGame.Click += new System.EventHandler(this.buttonStartGame_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.Location = new System.Drawing.Point(50, 557);
            this.buttonExit.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(300, 50);
            this.buttonExit.TabIndex = 1;
            this.buttonExit.Text = "Покинуть игру";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // DifficultySelectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(403, 649);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.buttonStartGame);
            this.Controls.Add(this.comboBoxHints);
            this.Controls.Add(this.labelHints);
            this.Controls.Add(this.groupBoxTimeLimit);
            this.Controls.Add(this.groupBoxStartingPlayer);
            this.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(425, 700);
            this.MinimumSize = new System.Drawing.Size(425, 700);
            this.Name = "DifficultySelectionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Меню";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DifficultySelectionForm_FormClosing);
            this.groupBoxStartingPlayer.ResumeLayout(false);
            this.groupBoxStartingPlayer.PerformLayout();
            this.groupBoxTimeLimit.ResumeLayout(false);
            this.groupBoxTimeLimit.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        // Компоненты формы
        private System.Windows.Forms.GroupBox groupBoxStartingPlayer;
        private System.Windows.Forms.RadioButton radioButtonComputer;
        private System.Windows.Forms.RadioButton radioButtonPlayer;
        private System.Windows.Forms.GroupBox groupBoxTimeLimit;
        private System.Windows.Forms.RadioButton radioButtonUnlimited;
        private System.Windows.Forms.RadioButton radioButtonNormal;
        private System.Windows.Forms.RadioButton radioButtonRush;
        private System.Windows.Forms.Label labelHints;
        private System.Windows.Forms.ComboBox comboBoxHints;
        private System.Windows.Forms.Button buttonStartGame;
        private System.Windows.Forms.Button buttonExit;
    }
}
