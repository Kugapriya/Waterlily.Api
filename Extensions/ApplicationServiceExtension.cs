using System;
using Microsoft.EntityFrameworkCore;
using Waterlily.Api.Data;
using Waterlily.Api.IRepository;
using Waterlily.Api.IService;
using Waterlily.Api.Repository;
using Waterlily.Api.Service;

namespace Waterlily.Api.Extensions;

public static class ApplicationServiceExtension
{
 public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
  
  services.AddControllers();
  services.AddDbContext<DataContext>(opt =>
        {
            opt.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        });
      services.AddCors();
    services.AddScoped<IEmployeeRepository, EmployeeRepository>();
    services.AddScoped<IEmployeeService,EmployeeService>();
    
       return services;
    }

}
