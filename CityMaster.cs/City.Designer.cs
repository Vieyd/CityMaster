namespace CityMaster
{
    partial class City
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
            this.components = new System.ComponentModel.Container();
            this.labelNextLetter = new System.Windows.Forms.Label();
            this.textBoxComputer = new System.Windows.Forms.TextBox();
            this.textBoxPlayer = new System.Windows.Forms.TextBox();
            this.buttonConfirm = new System.Windows.Forms.Button();
            this.buttonSurrender = new System.Windows.Forms.Button();
            this.buttonHint = new System.Windows.Forms.Button();
            this.labelHintsRemaining = new System.Windows.Forms.Label();
            this.labelTimer = new System.Windows.Forms.Label();
            this.gameTimer = new System.Windows.Forms.Timer(this.components);
            this.buttonExitGame = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.richTextBoxGameLog = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelNextLetter
            // 
            this.labelNextLetter.AutoSize = true;
            this.labelNextLetter.Location = new System.Drawing.Point(95, 197);
            this.labelNextLetter.Name = "labelNextLetter";
            this.labelNextLetter.Size = new System.Drawing.Size(341, 26);
            this.labelNextLetter.TabIndex = 0;
            this.labelNextLetter.Text = "(Необходимо начать с буквы - …)";
            // 
            // textBoxComputer
            // 
            this.textBoxComputer.Location = new System.Drawing.Point(52, 56);
            this.textBoxComputer.Name = "textBoxComputer";
            this.textBoxComputer.ReadOnly = true;
            this.textBoxComputer.Size = new System.Drawing.Size(400, 34);
            this.textBoxComputer.TabIndex = 5;
            // 
            // textBoxPlayer
            // 
            this.textBoxPlayer.Location = new System.Drawing.Point(52, 160);
            this.textBoxPlayer.Name = "textBoxPlayer";
            this.textBoxPlayer.Size = new System.Drawing.Size(400, 34);
            this.textBoxPlayer.TabIndex = 1;
            this.textBoxPlayer.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxPlayer_KeyPress);
            // 
            // buttonConfirm
            // 
            this.buttonConfirm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonConfirm.Location = new System.Drawing.Point(169, 252);
            this.buttonConfirm.Name = "buttonConfirm";
            this.buttonConfirm.Size = new System.Drawing.Size(164, 62);
            this.buttonConfirm.TabIndex = 8;
            this.buttonConfirm.Text = "Подтвердить";
            this.buttonConfirm.UseVisualStyleBackColor = true;
            this.buttonConfirm.Click += new System.EventHandler(this.buttonConfirm_Click);
            // 
            // buttonSurrender
            // 
            this.buttonSurrender.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonSurrender.Location = new System.Drawing.Point(274, 442);
            this.buttonSurrender.Name = "buttonSurrender";
            this.buttonSurrender.Size = new System.Drawing.Size(149, 62);
            this.buttonSurrender.TabIndex = 3;
            this.buttonSurrender.Text = "Сдаться";
            this.buttonSurrender.UseVisualStyleBackColor = true;
            this.buttonSurrender.Click += new System.EventHandler(this.buttonSurrender_Click);
            // 
            // buttonHint
            // 
            this.buttonHint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonHint.Location = new System.Drawing.Point(82, 442);
            this.buttonHint.Name = "buttonHint";
            this.buttonHint.Size = new System.Drawing.Size(149, 62);
            this.buttonHint.TabIndex = 2;
            this.buttonHint.Text = "Подсказка";
            this.buttonHint.UseVisualStyleBackColor = true;
            this.buttonHint.Click += new System.EventHandler(this.buttonHint_Click);
            // 
            // labelHintsRemaining
            // 
            this.labelHintsRemaining.AutoSize = true;
            this.labelHintsRemaining.Location = new System.Drawing.Point(38, 413);
            this.labelHintsRemaining.Name = "labelHintsRemaining";
            this.labelHintsRemaining.Size = new System.Drawing.Size(230, 26);
            this.labelHintsRemaining.TabIndex = 6;
            this.labelHintsRemaining.Text = "Осталось подсказок: 0";
            // 
            // labelTimer
            // 
            this.labelTimer.AutoSize = true;
            this.labelTimer.Location = new System.Drawing.Point(125, 343);
            this.labelTimer.Name = "labelTimer";
            this.labelTimer.Size = new System.Drawing.Size(260, 26);
            this.labelTimer.TabIndex = 7;
            this.labelTimer.Text = "Оставшееся время: -- сек";
            // 
            // gameTimer
            // 
            this.gameTimer.Interval = 1000;
            // 
            // buttonExitGame
            // 
            this.buttonExitGame.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonExitGame.Location = new System.Drawing.Point(52, 549);
            this.buttonExitGame.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.buttonExitGame.Name = "buttonExitGame";
            this.buttonExitGame.Size = new System.Drawing.Size(400, 74);
            this.buttonExitGame.TabIndex = 4;
            this.buttonExitGame.Text = "Покинуть игру";
            this.buttonExitGame.UseVisualStyleBackColor = true;
            this.buttonExitGame.Click += new System.EventHandler(this.buttonExitGame_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(151, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(200, 26);
            this.label1.TabIndex = 9;
            this.label1.Text = "Ответ компьютера:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(125, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(261, 26);
            this.label2.TabIndex = 10;
            this.label2.Text = "Поле ввода пользователя:";
            // 
            // richTextBoxGameLog
            // 
            this.richTextBoxGameLog.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.richTextBoxGameLog.Location = new System.Drawing.Point(560, 56);
            this.richTextBoxGameLog.Name = "richTextBoxGameLog";
            this.richTextBoxGameLog.ReadOnly = true;
            this.richTextBoxGameLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBoxGameLog.Size = new System.Drawing.Size(567, 567);
            this.richTextBoxGameLog.TabIndex = 11;
            this.richTextBoxGameLog.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(779, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(160, 26);
            this.label3.TabIndex = 14;
            this.label3.Text = "Протокол игры";
            // 
            // City
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1223, 674);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.richTextBoxGameLog);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonExitGame);
            this.Controls.Add(this.labelTimer);
            this.Controls.Add(this.labelHintsRemaining);
            this.Controls.Add(this.buttonHint);
            this.Controls.Add(this.buttonSurrender);
            this.Controls.Add(this.buttonConfirm);
            this.Controls.Add(this.textBoxPlayer);
            this.Controls.Add(this.textBoxComputer);
            this.Controls.Add(this.labelNextLetter);
            this.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1500, 725);
            this.MinimumSize = new System.Drawing.Size(518, 633);
            this.Name = "City";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Игра: Города";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.City_FormClosing);
            this.Load += new System.EventHandler(this.City_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        // Компоненты формы
        private System.Windows.Forms.Label labelNextLetter;
        private System.Windows.Forms.TextBox textBoxComputer;
        private System.Windows.Forms.TextBox textBoxPlayer;
        private System.Windows.Forms.Button buttonConfirm;
        private System.Windows.Forms.Button buttonSurrender;
        private System.Windows.Forms.Button buttonHint;
        private System.Windows.Forms.Label labelHintsRemaining;
        private System.Windows.Forms.Label labelTimer;
        private System.Windows.Forms.Timer gameTimer;
        private System.Windows.Forms.Button buttonExitGame;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox richTextBoxGameLog;
        private System.Windows.Forms.Label label3;
    }
}