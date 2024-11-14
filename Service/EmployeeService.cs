using System;
using Waterlily.Api.Entities;
using Waterlily.Api.IRepository;
using Waterlily.Api.IService;

namespace Waterlily.Api.Service;

public class EmployeeService:IEmployeeService
{
 public readonly IEmployeeRepository _employeeRepository;

    public EmployeeService(IEmployeeRepository employeeRepository)
    {
            _employeeRepository = employeeRepository;
    }
    public async Task<Employee> InsertEmployeeAsync(Employee employee)
    {
        return await _employeeRepository.InsertEmployee(employee);
    }

    public async Task DeleteEmployeeByIdAsync(int id)
    {
        await _employeeRepository.DeleteEmployee(id);
    }

    public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
    {
        return await _employeeRepository.GetAllEmployees();
    }

    public async Task<Employee> GetEmployeeByIdAsync(int id)
    {
        return await _employeeRepository.GetByEmployeeId(id);
    }

    public async Task<Employee> UpdateEmployeeAsync(Employee employee)
    {
        var updatedEmployee=await _employeeRepository.UpdateEmployee(employee);
        return updatedEmployee;
    }
}
