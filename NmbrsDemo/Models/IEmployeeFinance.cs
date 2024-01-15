namespace NmbrsDemo.Models
{
    public interface IEmployeeFinance
    {
        public decimal GrossAnnualSalary { get; }
        public decimal NetAnnualSalary { get; }
        public decimal AnnualCostToEmployer { get; }
    }
}
