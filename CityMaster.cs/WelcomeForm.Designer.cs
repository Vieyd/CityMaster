namespace CityMaster
{
    partial class WelcomeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WelcomeForm));
            this.labelWelcome = new System.Windows.Forms.Label();
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.labelWelcomeForm = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelWelcome
            // 
            this.labelWelcome.AutoSize = true;
            this.labelWelcome.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelWelcome.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelWelcome.Location = new System.Drawing.Point(60, 60);
            this.labelWelcome.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.labelWelcome.Name = "labelWelcome";
            this.labelWelcome.Size = new System.Drawing.Size(2, 27);
            this.labelWelcome.TabIndex = 2;
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(316, 401);
            this.buttonStart.Margin = new System.Windows.Forms.Padding(5);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(300, 100);
            this.buttonStart.TabIndex = 0;
            this.buttonStart.Text = "Начать путешествие";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.Location = new System.Drawing.Point(738, 401);
            this.buttonExit.Margin = new System.Windows.Forms.Padding(5);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(300, 100);
            this.buttonExit.TabIndex = 1;
            this.buttonExit.Text = "Отказ от игры";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // labelWelcomeForm
            // 
            this.labelWelcomeForm.BackColor = System.Drawing.SystemColors.Control;
            this.labelWelcomeForm.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelWelcomeForm.Location = new System.Drawing.Point(70, 48);
            this.labelWelcomeForm.MaximumSize = new System.Drawing.Size(1274, 308);
            this.labelWelcomeForm.MinimumSize = new System.Drawing.Size(1274, 308);
            this.labelWelcomeForm.Name = "labelWelcomeForm";
            this.labelWelcomeForm.Size = new System.Drawing.Size(1274, 308);
            this.labelWelcomeForm.TabIndex = 3;
            this.labelWelcomeForm.Text = resources.GetString("labelWelcomeForm.Text");
            // 
            // WelcomeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 26F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1426, 539);
            this.Controls.Add(this.labelWelcomeForm);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.labelWelcome);
            this.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1448, 590);
            this.MinimumSize = new System.Drawing.Size(1428, 590);
            this.Name = "WelcomeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Добро пожаловать";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        // Компоненты формы
        private System.Windows.Forms.Label labelWelcome;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Label labelWelcomeForm;
    }

}
