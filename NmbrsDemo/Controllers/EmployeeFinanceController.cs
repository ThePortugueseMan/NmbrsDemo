using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NmbrsDemo.Logic;
using NmbrsDemo.Models;

namespace NmbrsDemo.Controllers;

[ApiController]
[Route("employeefinance")]
public class EmployeeFinanceController : ControllerBase
{
    private readonly ILogger<EmployeeFinanceController> _logger;

    public EmployeeFinanceController(ILogger<EmployeeFinanceController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IEmployeeFinance GetFinanceFromId([FromQuery] string employeeid)
    {
        return EmployeeFinanceLogic.GetEmployeeFinanceFromId(employeeid);
    }

    [HttpPost]
    public async Task<bool> InsertOrUpdateGrossAnnualSalaryById([FromQuery] string employeeid)
    {
        JObject jsonBody;
        decimal grossAnnualSalary = 0M;

        try
        {
            using (StreamReader reader = new StreamReader(Request.Body))
            {
                string body = await reader.ReadToEndAsync();
                jsonBody = JsonConvert.DeserializeObject<JObject>(body);
            }

            grossAnnualSalary = Decimal.Parse((string)jsonBody["grossAnnualSalary"]);
            return EmployeeFinanceLogic.InsertOrUpdateGrossAnnualSalaryById(employeeid, grossAnnualSalary);
        }
        catch(Exception e)
        {
            return false;
        }
    }
}