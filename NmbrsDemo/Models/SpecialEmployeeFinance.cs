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
        netAnnualSalary = grossAnnualSalary * EmployeePercentageOnGross;
    }
}
