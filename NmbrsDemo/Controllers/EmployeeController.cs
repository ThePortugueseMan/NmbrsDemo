using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using NmbrsDemo.Logic;
using NmbrsDemo.Models;

namespace NmbrsDemo.Controllers;

[ApiController]
[Route("employee")]
public class EmployeeController : ControllerBase
{
    private readonly ILogger<EmployeeController> _logger;

    public EmployeeController(ILogger<EmployeeController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public List<Employee> GetList()
    {
        return EmployeeInfo.GetEmployeeList();
    }

    [HttpPost]
    public bool NewEmployee(Employee employee)
    {
        return EmployeeInfo.AddNewEmployee(employee);
    }


    [HttpDelete]
    public bool RemoveEmployeeByEmployeeId([FromQuery] string employeeid)
    {
        return EmployeeInfo.RemoveEmployeeByID(employeeid);
    }
}