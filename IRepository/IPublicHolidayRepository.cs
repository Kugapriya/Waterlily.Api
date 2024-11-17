using System;
using Waterlily.Api.Entities;

namespace Waterlily.Api.IRepository;

public interface IPublicHolidayRepository
{
 Task<PublicHoliday> InsertHoliday(PublicHoliday holiday);

 Task<IEnumerable<PublicHoliday>> GetAllHolidays();
 Task<PublicHoliday> GetPublicHolidayByDate(DateOnly date);
 Task<PublicHoliday> UpdateHoliday(PublicHoliday holiday);
 Task DeleteHoliday(int id);
 Task<List<PublicHoliday>> GetPublicHolidaysBetweenDates(DateOnly startDate, DateOnly endDate);

}
