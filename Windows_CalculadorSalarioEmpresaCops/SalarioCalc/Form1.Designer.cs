namespace SalarioCalc
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox baseSalaryTextBox;
        private System.Windows.Forms.TextBox totalHoursTextBox;
        private System.Windows.Forms.TextBox nightHoursTextBox;
        private System.Windows.Forms.TextBox holidayHoursTextBox;
        private System.Windows.Forms.Button calculateButton;
        private System.Windows.Forms.Label resultLabel;

        /// <summary>
        /// Limpar os recursos usados.
        /// </summary>
        /// <param name="disposing">true se os recursos gerenciados devem ser descartados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.baseSalaryTextBox = new System.Windows.Forms.TextBox();
            this.totalHoursTextBox = new System.Windows.Forms.TextBox();
            this.nightHoursTextBox = new System.Windows.Forms.TextBox();
            this.holidayHoursTextBox = new System.Windows.Forms.TextBox();
            this.calculateButton = new System.Windows.Forms.Button();
            this.resultLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // baseSalaryTextBox
            // 
            this.baseSalaryTextBox.Location = new System.Drawing.Point(30, 30);
            this.baseSalaryTextBox.Name = "baseSalaryTextBox";
            this.baseSalaryTextBox.ReadOnly = true;
            this.baseSalaryTextBox.Size = new System.Drawing.Size(200, 20);
            this.baseSalaryTextBox.TabIndex = 0;
            // 
            // totalHoursTextBox
            // 
            this.totalHoursTextBox.Location = new System.Drawing.Point(30, 70);
            this.totalHoursTextBox.Name = "totalHoursTextBox";
            this.totalHoursTextBox.Size = new System.Drawing.Size(200, 20);
            this.totalHoursTextBox.TabIndex = 1;
            this.totalHoursTextBox.Enter += new System.EventHandler(this.TextBox_Enter);
            this.totalHoursTextBox.Leave += new System.EventHandler(this.TextBox_Leave);
            // 
            // nightHoursTextBox
            // 
            this.nightHoursTextBox.Location = new System.Drawing.Point(30, 110);
            this.nightHoursTextBox.Name = "nightHoursTextBox";
            this.nightHoursTextBox.Size = new System.Drawing.Size(200, 20);
            this.nightHoursTextBox.TabIndex = 2;
            this.nightHoursTextBox.Enter += new System.EventHandler(this.TextBox_Enter);
            this.nightHoursTextBox.Leave += new System.EventHandler(this.TextBox_Leave);
            // 
            // holidayHoursTextBox
            // 
            this.holidayHoursTextBox.Location = new System.Drawing.Point(30, 150);
            this.holidayHoursTextBox.Name = "holidayHoursTextBox";
            this.holidayHoursTextBox.Size = new System.Drawing.Size(200, 20);
            this.holidayHoursTextBox.TabIndex = 3;
            this.holidayHoursTextBox.Enter += new System.EventHandler(this.TextBox_Enter);
            this.holidayHoursTextBox.Leave += new System.EventHandler(this.TextBox_Leave);
            // 
            // calculateButton
            // 
            this.calculateButton.Location = new System.Drawing.Point(30, 190);
            this.calculateButton.Name = "calculateButton";
            this.calculateButton.Size = new System.Drawing.Size(200, 23);
            this.calculateButton.TabIndex = 4;
            this.calculateButton.Text = "Calcular";
            this.calculateButton.UseVisualStyleBackColor = true;
            this.calculateButton.Click += new System.EventHandler(this.OnCalculateButtonClick);
            // 
            // resultLabel
            // 
            this.resultLabel.AutoSize = true;
            this.resultLabel.Location = new System.Drawing.Point(30, 230);
            this.resultLabel.MaximumSize = new System.Drawing.Size(400, 0);
            this.resultLabel.Name = "resultLabel";
            this.resultLabel.Size = new System.Drawing.Size(70, 13);
            this.resultLabel.TabIndex = 5;
            this.resultLabel.Text = "Salário Final: ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 480);
            this.Controls.Add(this.resultLabel);
            this.Controls.Add(this.calculateButton);
            this.Controls.Add(this.holidayHoursTextBox);
            this.Controls.Add(this.nightHoursTextBox);
            this.Controls.Add(this.totalHoursTextBox);
            this.Controls.Add(this.baseSalaryTextBox);
            this.Name = "Form1";
            this.Text = "Calculadora de Salário para empresa COPS";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}
