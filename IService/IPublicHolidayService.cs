using System;
using Waterlily.Api.Entities;

namespace Waterlily.Api.IService;

public interface IPublicHolidayService
{
        Task<IEnumerable<PublicHoliday>> GetAllHolidaysAsync();
        Task<PublicHoliday> GetHolidayByDateAsync(DateOnly date);
        Task<PublicHoliday> InsertHolidayAsync(PublicHoliday holiday);
        Task<PublicHoliday> UpdateHolidayAsync(PublicHoliday holiday);
       Task DeleteHolidayAsync(int id);
       Task<int> CalculateWorkingDays(DateOnly startDate, DateOnly endDate);
}
