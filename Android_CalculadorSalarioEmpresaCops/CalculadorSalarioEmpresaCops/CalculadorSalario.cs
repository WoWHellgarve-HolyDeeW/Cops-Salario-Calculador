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
        private const double holidayProportion = 76.04; // Proporção fixa para subsídio de férias
        private const double christmasProportion = 76.04; // Proporção fixa para subsídio de natal
        private const double irsRate = 0.06; // Taxa de IRS específica do seu caso
        private const double socialSecurityRate = 0.11; // Taxa de Segurança Social
        private const int standardMonthlyHours = 160; // Standard full-time monthly hours
        private const double firstOvertimeRate = 1.25; // 125% of base salary for the first overtime hour
        private const double subsequentOvertimeRate = 1.50; // 150% of base salary for subsequent overtime hours

        public string CalculateSalary(double totalHours, double nightHours, double holidayHours)
        {
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

            // Cálculo de horas extras
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

            // Gerar relatório final
            return GenerateSalaryBreakdown(finalSalary, normalPay, nightPay, holidayPay, foodAllowance, totalProvisions, totalDeductions, irsDeduction, socialSecurityDeduction, overtimeHours, firstOvertimePay, subsequentOvertimePay, totalOvertimePay);
        }

        private string GenerateSalaryBreakdown(double finalSalary, double normalPay, double nightPay, double holidayPay, double foodAllowance, double totalProvisions, double totalDeductions, double irsDeduction, double socialSecurityDeduction, double overtimeHours, double firstOvertimePay, double subsequentOvertimePay, double totalOvertimePay)
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
            if (overtimeHours > 0)
            {
                breakdown.AppendLine($"Atenção! Você trabalhou {overtimeHours} horas extras.");
                breakdown.AppendLine($"Primeira hora extra paga a 125%: {firstOvertimePay.ToString("C")}");
                breakdown.AppendLine($"Horas extras subsequentes pagas a 150%: {subsequentOvertimePay.ToString("C")}");
                breakdown.AppendLine($"Total a receber pelas horas extras: {totalOvertimePay.ToString("C")}. Este valor é apenas sobre as horas extra trabalhadas não contém: Horas Feriados, descontos de IRS ou Taxa Social.");
            }
            return breakdown.ToString();
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
