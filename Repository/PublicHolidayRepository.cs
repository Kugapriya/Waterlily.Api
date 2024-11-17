using System;
using Microsoft.EntityFrameworkCore;
using Waterlily.Api.Data;
using Waterlily.Api.Entities;
using Waterlily.Api.IRepository;

namespace Waterlily.Api.Repository;

public class PublicHolidayRepository : IPublicHolidayRepository
{
    private readonly DataContext _context;
    public PublicHolidayRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PublicHoliday>> GetAllHolidays()
    {
       return await _context.Holidays.ToListAsync();
    }

    public async Task<PublicHoliday> GetPublicHolidayByDate(DateOnly date)
    {
        var holiday=await _context.Holidays.FindAsync(date);
        if(holiday==null){
        throw new Exception($"{date} is not Holiday.");
        }
        return holiday;
    }

public async Task<PublicHoliday> InsertHoliday(PublicHoliday holiday)
{
    
    var existingHoliday = await _context.Holidays
        .FirstOrDefaultAsync(h => h.HolidayDate == holiday.HolidayDate);

    if (existingHoliday != null)
    {
        
        throw new InvalidOperationException("A holiday with this date already exists.");
    }

    await _context.Holidays.AddAsync(holiday);
    await _context.SaveChangesAsync();
    return holiday;
}



     public async Task<List<PublicHoliday>> GetPublicHolidaysBetweenDates(DateOnly startDate, DateOnly endDate)
    {
        return await _context.Holidays
            .Where(h => h.HolidayDate >= startDate && h.HolidayDate <= endDate)
            .ToListAsync();
    }

    public  async Task<PublicHoliday> UpdateHoliday(PublicHoliday holiday)
    {
       var existingHoliday=await _context.Holidays.FindAsync(holiday.id);
      if(existingHoliday==null)
      {
         throw new Exception($"Holiday with ID {holiday.id} not Found");
      }
        existingHoliday.HolidayDate=holiday.HolidayDate;
        existingHoliday.Reason=holiday.Reason;
        await _context.SaveChangesAsync();
        return existingHoliday;
    }

    public async Task DeleteHoliday(int id)
    {
       var holiday=await _context.Holidays.FindAsync(id);
        if(holiday!=null)
        {
            _context.Holidays.Remove(holiday);
            await _context.SaveChangesAsync();
        }
    }
}


