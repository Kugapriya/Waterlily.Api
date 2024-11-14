using System;
using Microsoft.AspNetCore.Mvc;
using Waterlily.Api.Entities;
using Waterlily.Api.IService;

namespace Waterlily.Api.Controllers;

public class EmployeeController:BaseApiController
{
  public readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        
      [HttpPost("insertEmployee")]
     public async Task<IActionResult> InsertEmployee(Employee employee)
       {
       await _employeeService.InsertEmployeeAsync(employee);
        return Ok();
       }

     [HttpGet("getAllEmployees")]
    public async Task<IEnumerable<Employee>> GetAllEmployees()
      {
       return  await _employeeService.GetAllEmployeesAsync();
      }

      [HttpGet("getEmployeeById/{id}")]
      public async Task<Employee> GetEmployeeById(int id)
      {
        return await _employeeService.GetEmployeeByIdAsync(id);
      }

      [HttpDelete("deleteEmployee/{id}")]
      public async Task<IActionResult> DeleteEmployeeById(int id)
      {
         await _employeeService.DeleteEmployeeByIdAsync(id);
         return Ok();
      }

      [HttpPut("updateEmployee/{id}")]
      public async Task<IActionResult> UpdateEmployee(int id,Employee employee)
      {
        if(id!=employee.Id)
        {
           return BadRequest("Employee ID mismatch");
        }
        var updatedEmployee=await _employeeService.UpdateEmployeeAsync(employee);
        return Ok(updatedEmployee);
      }

}

