namespace NmbrsDemo.Models
{
    public class EmployeeBasicInfo
    {
        public string EmployeeId { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string EmployeeTypeId { get; set; } = string.Empty;

        public enum EmployeeType
        {
            Regular,
            Special
        }
    }
}
