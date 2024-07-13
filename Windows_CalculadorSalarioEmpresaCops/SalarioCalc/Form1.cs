using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SalarioCalc
{
    public partial class Form1 : Form
    {
        private const double hourlyBaseSalary = 5.26;
        private const double dailyFoodAllowance = 7.05;
        private const double nightHourRate = 1.32;
        private const double holidayHourRate = 5.26;
        private const double holidayProportion = 76.04; // Proporção fixa para subsídio de férias
        private const double christmasProportion = 76.04; // Proporção fixa para subsídio de natal
        private const double irsRate = 0.06; // Taxa de IRS específica do seu caso
        private const double socialSecurityRate = 0.11; // Taxa de Segurança Social
        private const int standardMonthlyHours = 160; // Standard full-time monthly hours
        private const double firstOvertimeRate = 1.25; // 125% of base salary for the first overtime hour
        private const double subsequentOvertimeRate = 1.50; // 150% of base salary for subsequent overtime hours

        public Form1()
        {
            InitializeComponent();
            baseSalaryTextBox.Text = hourlyBaseSalary.ToString("C");
            InitializePlaceholders();
        }

        private void OnCalculateButtonClick(object sender, EventArgs e)
        {
            try
            {
                double totalHours = double.Parse(totalHoursTextBox.Text);
                double nightHours = double.Parse(nightHoursTextBox.Text);
                double holidayHours = double.Parse(holidayHoursTextBox.Text);
                int workDays = (int)(totalHours / 8); // Assumindo dias de trabalho de 8 horas

                double normalPay = totalHours * hourlyBaseSalary;
                double nightPay = nightHours * nightHourRate;
                double holidayPay = holidayHours * holidayHourRate;
                double foodAllowance = workDays * dailyFoodAllowance; // Subsídio de alimentação em cartão
                double totalProvisions = holidayProportion + christmasProportion; // Usando valor fixo para subsídio de férias e natal
                double grossSalary = normalPay + nightPay + holidayPay + totalProvisions;

                // Deduzir o valor do subsídio de alimentação para cálculo do IRS
                double taxableSalaryForIRS = grossSalary - foodAllowance;
                double irsDeduction = CalculateIRSDeduction(taxableSalaryForIRS);

                // Segurança Social aplicada ao salário bruto, incluindo provisões
                double socialSecurityDeduction = CalculateSocialSecurityDeduction(grossSalary);
                double totalDeductions = irsDeduction + socialSecurityDeduction;

                double finalSalary = grossSalary - totalDeductions;

                resultLabel.Text = GenerateSalaryBreakdown(finalSalary, normalPay, nightPay, holidayPay, foodAllowance, totalProvisions, totalDeductions, irsDeduction, socialSecurityDeduction);

                // Overtime calculations
                double overtimeHours = totalHours - standardMonthlyHours;
                if (overtimeHours > 0)
                {
                    double firstOvertimePay = 0;
                    double subsequentOvertimePay = 0;
                    if (overtimeHours <= 1)
                    {
                        firstOvertimePay = overtimeHours * hourlyBaseSalary * firstOvertimeRate;
                    }
                    else
                    {
                        firstOvertimePay = 1 * hourlyBaseSalary * firstOvertimeRate;
                        subsequentOvertimePay = (overtimeHours - 1) * hourlyBaseSalary * subsequentOvertimeRate;
                    }
                    double totalOvertimePay = firstOvertimePay + subsequentOvertimePay;
                    ShowOvertimeAlert(overtimeHours, firstOvertimePay, subsequentOvertimePay, totalOvertimePay);
                }

                MessageBox.Show("Os cálculos são uma estimativa. Até à data, a margem de erro no salário final conhecida é de 1-2 euros.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao calcular: " + ex.Message);
            }
        }

        private void ShowOvertimeAlert(double overtimeHours, double firstOvertimePay, double subsequentOvertimePay, double totalOvertimePay)
        {
            StringBuilder alertMessage = new StringBuilder();
            alertMessage.AppendLine($"Atenção! Você trabalhou {overtimeHours} horas extras.");
            alertMessage.AppendLine($"Primeira hora extra paga a 125%: {firstOvertimePay.ToString("C")}");
            alertMessage.AppendLine($"Horas extras subsequentes pagas a 150%: {subsequentOvertimePay.ToString("C")}");
            alertMessage.AppendLine($"Total a receber pelas horas extras: {totalOvertimePay.ToString("C")}. Este valor é apenas sobre as horas extra trabalhadas não contém : Horas Feriados, descontos de IRS ou Taxa Social ");
            resultLabel.Text += Environment.NewLine + alertMessage.ToString();
        }

        private string GenerateSalaryBreakdown(double finalSalary, double normalPay, double nightPay, double holidayPay, double foodAllowance, double totalProvisions, double totalDeductions, double irsDeduction, double socialSecurityDeduction)
        {
            StringBuilder breakdown = new StringBuilder();
            breakdown.AppendLine($"Salário Final (com descontos): {finalSalary.ToString("C")}");
            breakdown.AppendLine($"Salário Base (Horas Normais): {normalPay.ToString("C")}");
            breakdown.AppendLine($"Adicional Noturno: {nightPay.ToString("C")}");
            breakdown.AppendLine($"Adicional Feriado: {holidayPay.ToString("C")}");
            breakdown.AppendLine($"Subsidio de Alimentação (em cartão): {foodAllowance.ToString("C")} (Não incluído no salário líquido)");
            breakdown.AppendLine($"Proporcional de Férias e Natal: {totalProvisions.ToString("C")}");
            breakdown.AppendLine($"Total de Descontos: {totalDeductions.ToString("C")}");
            breakdown.AppendLine($"Desconto IRS: {irsDeduction.ToString("C")}");
            breakdown.AppendLine($"Desconto Segurança Social: {socialSecurityDeduction.ToString("C")}");
            return breakdown.ToString();
        }

        private void InitializePlaceholders()
        {
            SetPlaceholder(totalHoursTextBox, "Total de Horas");
            SetPlaceholder(nightHoursTextBox, "Horas Noturnas (22h-06h)");
            SetPlaceholder(holidayHoursTextBox, "Horas de Feriado");
        }

        private void SetPlaceholder(TextBox textBox, string placeholder)
        {
            textBox.Tag = placeholder;
            textBox.Text = placeholder;
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Enter += TextBox_Enter;
            textBox.Leave += TextBox_Leave;
        }

        private void TextBox_Enter(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && textBox.Text == (string)textBox.Tag)
            {
                textBox.Text = "";
                textBox.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void TextBox_Leave(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = (string)textBox.Tag;
                textBox.ForeColor = System.Drawing.Color.Gray;
            }
        }

        private double CalculateIRSDeduction(double taxableSalary)
        {
            return taxableSalary * irsRate;
        }

        private double CalculateSocialSecurityDeduction(double grossSalary)
        {
            return grossSalary * socialSecurityRate; // Segurança Social = 11%
        }
    }
}
