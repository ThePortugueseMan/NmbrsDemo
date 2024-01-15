namespace NmbrsDemo.Models;

public class SpecialEmployeeFinance : EmployeeFinance
{
    private decimal EmployerDiscountOnAnnual { get; set; } = 100M;
    public SpecialEmployeeFinance(decimal grossAnnualSalary) 
    {
        EmployerCostPercentageOnGross = 1.1M;
        EmployeePercentageOnGross = 0.9M;
        GrossAnnualSalary = grossAnnualSalary;
    }

    public override void CalculateFields()
    {
        annualCostToEmployer = (grossAnnualSalary * EmployerCostPercentageOnGross) - EmployerDiscountOnAnnual;
        if (annualCostToEmployer < 0) annualCostToEmployer = 0;

        netAnnualSalary = grossAnnualSalary * EmployeePercentageOnGross;
        if (netAnnualSalary < 0) netAnnualSalary = 0;
    }
}
