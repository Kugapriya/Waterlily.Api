using System;
using Waterlily.Api.Entities;

namespace Waterlily.Api.IService;

public interface IEmployeeService
{
 Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        Task<Employee> GetEmployeeByIdAsync(int id);
        Task<Employee> InsertEmployeeAsync(Employee employee);
        Task<Employee> UpdateEmployeeAsync(Employee employee);
        Task DeleteEmployeeByIdAsync(int id);
}
