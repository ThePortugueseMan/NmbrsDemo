using Microsoft.AspNetCore.Mvc;
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
    public bool InsertOrUpdateGrossAnnualSalaryById([FromQuery] string employeeid)
    {
        string body = Request.Body.ToString();
        decimal grossAnnualSalary = Decimal.Parse(body);
        return EmployeeFinanceLogic.InsertOrUpdateGrossAnnualSalaryById(employeeid, grossAnnualSalary);
    }
}