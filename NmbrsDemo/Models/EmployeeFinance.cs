namespace NmbrsDemo.Models;

public abstract class EmployeeFinance : IEmployeeFinance
{
    protected decimal grossAnnualSalary;
    public decimal GrossAnnualSalary
    {
        set
        {
            grossAnnualSalary = value;
            CalculateFields();
        }
        get
        {
            return grossAnnualSalary;
        }
    }
    protected decimal netAnnualSalary;
    public decimal NetAnnualSalary 
    {
        get { return netAnnualSalary; } 
    }
    protected decimal annualCostToEmployer;
    public decimal AnnualCostToEmployer 
    {
        get { return annualCostToEmployer; }
    }

    protected decimal EmployerCostPercentageOnGross { get; set; } = 1.3M;
    protected decimal EmployeePercentageOnGross { get; set; } = 0.7M;

    public virtual void CalculateFields()
    {
        annualCostToEmployer = GrossAnnualSalary * EmployerCostPercentageOnGross;
        netAnnualSalary = GrossAnnualSalary * EmployeePercentageOnGross;
    }
}