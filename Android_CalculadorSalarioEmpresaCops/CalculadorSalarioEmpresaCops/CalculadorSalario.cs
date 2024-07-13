using System;
using System.Text;
using static Google.Android.Material.BottomSheet.BottomSheetBehavior;

namespace CalculadorSalarioEmpresaCops
{
    public class CalculadorSalario
    {
        private const double hourlyBaseSalary = 5.26;
        private const double dailyFoodAllowance = 7.05;
        private const double nightHourRate = 1.32;
        private const double holidayHourRate = 5.26;
        private const double holidayProportion = 76.04; // Propor��o fixa para subs�dio de f�rias
        private const double christmasProportion = 76.04; // Propor��o fixa para subs�dio de natal
        private const double irsRate = 0.06; // Taxa de IRS espec�fica do seu caso
        private const double socialSecurityRate = 0.11; // Taxa de Seguran�a Social
        private const int standardMonthlyHours = 160; // Standard full-time monthly hours
        private const double firstOvertimeRate = 1.25; // 125% of base salary for the first overtime hour
        private const double subsequentOvertimeRate = 1.50; // 150% of base salary for subsequent overtime hours

        public string CalculateSalary(double totalHours, double nightHours, double holidayHours)
        {
            int workDays = (int)(totalHours / 8); // Assumindo dias de trabalho de 8 horas

            double normalPay = totalHours * hourlyBaseSalary;
            double nightPay = nightHours * nightHourRate;
            double holidayPay = holidayHours * holidayHourRate;
            double foodAllowance = workDays * dailyFoodAllowance; // Subs�dio de alimenta��o em cart�o
            double totalProvisions = holidayProportion + christmasProportion; // Usando valor fixo para subs�dio de f�rias e natal
            double grossSalary = normalPay + nightPay + holidayPay + totalProvisions;

            // Deduzir o valor do subs�dio de alimenta��o para c�lculo do IRS
            double taxableSalaryForIRS = grossSalary - foodAllowance;
            double irsDeduction = CalculateIRSDeduction(taxableSalaryForIRS);

            // Seguran�a Social aplicada ao sal�rio bruto, incluindo provis�es
            double socialSecurityDeduction = CalculateSocialSecurityDeduction(grossSalary);
            double totalDeductions = irsDeduction + socialSecurityDeduction;

            double finalSalary = grossSalary - totalDeductions;

            // C�lculo de horas extras
            double overtimeHours = totalHours - standardMonthlyHours;
            double firstOvertimePay = 0;
            double subsequentOvertimePay = 0;
            double totalOvertimePay = 0;
            if (overtimeHours > 0)
            {
                if (overtimeHours <= 1)
                {
                    firstOvertimePay = overtimeHours * hourlyBaseSalary * firstOvertimeRate;
                }
                else
                {
                    firstOvertimePay = 1 * hourlyBaseSalary * firstOvertimeRate;
                    subsequentOvertimePay = (overtimeHours - 1) * hourlyBaseSalary * subsequentOvertimeRate;
                }
                totalOvertimePay = firstOvertimePay + subsequentOvertimePay;
            }

            // Gerar relat�rio final
            return GenerateSalaryBreakdown(finalSalary, normalPay, nightPay, holidayPay, foodAllowance, totalProvisions, totalDeductions, irsDeduction, socialSecurityDeduction, overtimeHours, firstOvertimePay, subsequentOvertimePay, totalOvertimePay);
        }

        private string GenerateSalaryBreakdown(double finalSalary, double normalPay, double nightPay, double holidayPay, double foodAllowance, double totalProvisions, double totalDeductions, double irsDeduction, double socialSecurityDeduction, double overtimeHours, double firstOvertimePay, double subsequentOvertimePay, double totalOvertimePay)
        {
            StringBuilder breakdown = new StringBuilder();
            breakdown.AppendLine($"Sal�rio Final (com descontos): {finalSalary.ToString("C")}");
            breakdown.AppendLine($"Sal�rio Base (Horas Normais): {normalPay.ToString("C")}");
            breakdown.AppendLine($"Adicional Noturno: {nightPay.ToString("C")}");
            breakdown.AppendLine($"Adicional Feriado: {holidayPay.ToString("C")}");
            breakdown.AppendLine($"Subsidio de Alimenta��o (em cart�o): {foodAllowance.ToString("C")} (N�o inclu�do no sal�rio l�quido)");
            breakdown.AppendLine($"Proporcional de F�rias e Natal: {totalProvisions.ToString("C")}");
            breakdown.AppendLine($"Total de Descontos: {totalDeductions.ToString("C")}");
            breakdown.AppendLine($"Desconto IRS: {irsDeduction.ToString("C")}");
            breakdown.AppendLine($"Desconto Seguran�a Social: {socialSecurityDeduction.ToString("C")}");
            if (overtimeHours > 0)
            {
                breakdown.AppendLine($"Aten��o! Voc� trabalhou {overtimeHours} horas extras.");
                breakdown.AppendLine($"Primeira hora extra paga a 125%: {firstOvertimePay.ToString("C")}");
                breakdown.AppendLine($"Horas extras subsequentes pagas a 150%: {subsequentOvertimePay.ToString("C")}");
                breakdown.AppendLine($"Total a receber pelas horas extras: {totalOvertimePay.ToString("C")}. Este valor � apenas sobre as horas extra trabalhadas n�o cont�m: Horas Feriados, descontos de IRS ou Taxa Social.");
            }
            return breakdown.ToString();
        }

        private double CalculateIRSDeduction(double taxableSalary)
        {
            return taxableSalary * irsRate;
        }

        private double CalculateSocialSecurityDeduction(double grossSalary)
        {
            return grossSalary * socialSecurityRate; // Seguran�a Social = 11%
        }
    }
}
