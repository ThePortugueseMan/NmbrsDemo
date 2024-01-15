namespace NmbrsDemo.Models;

public class RegularEmployeeFinance : EmployeeFinance
{
    public RegularEmployeeFinance(decimal grossAnnualSalary) 
    {
        EmployerCostPercentageOnGross = 1.3M;
        EmployeePercentageOnGross = 0.7M;
        GrossAnnualSalary = grossAnnualSalary;
    }

    public override void CalculateFields()
    {
        annualCostToEmployer = GrossAnnualSalary * EmployerCostPercentageOnGross;
        netAnnualSalary = GrossAnnualSalary * EmployeePercentageOnGross;
    }
}
