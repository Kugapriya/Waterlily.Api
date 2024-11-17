using System;
using Waterlily.Api.Entities;
using Waterlily.Api.IRepository;
using Waterlily.Api.IService;

namespace Waterlily.Api.Service;

public class PublicHolidayService : IPublicHolidayService
{
     public readonly IPublicHolidayRepository _publicholidayRepository;

    public PublicHolidayService(IPublicHolidayRepository publicHolidayRepository)
    {
            _publicholidayRepository = publicHolidayRepository;
    }

    public async Task<IEnumerable<PublicHoliday>> GetAllHolidaysAsync()
    {
         return await _publicholidayRepository.GetAllHolidays();
    }

    public async Task<PublicHoliday> GetHolidayByDateAsync(DateOnly date)
    {
           return await _publicholidayRepository.GetPublicHolidayByDate(date);
    }

    public async Task<PublicHoliday> InsertHolidayAsync(PublicHoliday holiday)
    {
        return await _publicholidayRepository.InsertHoliday(holiday);
    }

           public async Task<int> CalculateWorkingDays(DateOnly startDate, DateOnly endDate)
        {
          
            var publicHolidays = await _publicholidayRepository.GetPublicHolidaysBetweenDates(startDate, endDate);
            int workingDays = 0;

            for (DateOnly currentDate = startDate; currentDate <= endDate; currentDate = currentDate.AddDays(1))
            {
                if (currentDate.DayOfWeek == DayOfWeek.Saturday || currentDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    continue;
                }
                bool isHoliday = publicHolidays.Any(h => h.HolidayDate == currentDate);
                if (!isHoliday)
                {
                  
                    workingDays++;
                }
            }
            return workingDays;
        }

    public async Task<PublicHoliday> UpdateHolidayAsync(PublicHoliday holiday)
    {
        var updatedHoliday = await _publicholidayRepository.UpdateHoliday(holiday);

                if (updatedHoliday == null)
                {
                    throw new Exception("Holiday not found");
                }

                return updatedHoliday;
    }

    public async Task DeleteHolidayAsync(int id)
    {
        await _publicholidayRepository.DeleteHoliday(id);
    }
}
