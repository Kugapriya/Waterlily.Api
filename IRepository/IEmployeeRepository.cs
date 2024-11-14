using System;
using Waterlily.Api.Entities;

namespace Waterlily.Api.IRepository;

public interface IEmployeeRepository
{
 Task<Employee> InsertEmployee(Employee employee);
    Task<IEnumerable<Employee>> GetAllEmployees();
    Task<Employee> GetByEmployeeId(int id);
    Task<Employee> UpdateEmployee(Employee employee);
    Task DeleteEmployee(int id);
}
