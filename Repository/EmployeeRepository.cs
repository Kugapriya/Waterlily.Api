using System;
using Microsoft.EntityFrameworkCore;
using Waterlily.Api.Data;
using Waterlily.Api.Entities;
using Waterlily.Api.IRepository;

namespace Waterlily.Api.Repository;

public class EmployeeRepository:IEmployeeRepository
{
private readonly DataContext _context;
    public EmployeeRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<Employee> InsertEmployee(Employee employee)
    {
          await _context.Employees.AddAsync(employee);
         await _context.SaveChangesAsync();
         return employee;
    }

    public async Task DeleteEmployee(int id)
    {
        var employee=await _context.Employees.FindAsync(id);
        if(employee!=null)
        {
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Employee>> GetAllEmployees()
    {
        return await _context.Employees.ToListAsync();
    }

    public async Task<Employee> GetByEmployeeId(int id)
    {
      var employee= await _context.Employees.FindAsync(id);
      if(employee==null){
        throw new Exception($"Employee with ID {id} not Found.");
      }
      return employee;
    }

    public async Task<Employee> UpdateEmployee(Employee employee)
    {
      var existingEmployee=await _context.Employees.FindAsync(employee.Id);
      if(existingEmployee==null)
      {
         throw new Exception($"Employee with ID {employee.Id} not Found");
      }
        existingEmployee.Name=employee.Name;
        existingEmployee.Email=employee.Email;
        existingEmployee.JobPosition=employee.JobPosition;
        await _context.SaveChangesAsync();
        return existingEmployee;
    }
}
