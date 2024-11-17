using System;
using Microsoft.AspNetCore.Mvc;
using Waterlily.Api.Entities;
using Waterlily.Api.IService;

namespace Waterlily.Api.Controllers;
  [Route("api/[controller]")]
  [ApiController]
public class PublicHolidayController:BaseApiController
{
  public readonly IPublicHolidayService _publicholidayService;

        public PublicHolidayController(IPublicHolidayService publichoidayService)
        {
            _publicholidayService = publichoidayService;
        }

        [HttpGet("getAllHolidays")]
        public async Task<IEnumerable<PublicHoliday>> GetAllEmployees()
        {
            return  await _publicholidayService.GetAllHolidaysAsync();
        }
         [HttpPost("insertholiday")]
       public async Task<IActionResult> InsertHoliday(PublicHoliday holiday)
       {
       await _publicholidayService.InsertHolidayAsync(holiday);
        return Ok();
       }

       [HttpGet("CalculateWorkingDays")]
        public async Task<IActionResult> CalculateWorkingDays(DateOnly startDate, DateOnly endDate)
        {
            if (startDate > endDate)
            {
                return BadRequest("Start date cannot be later than end date.");
            }

            var workingDays = await _publicholidayService.CalculateWorkingDays(startDate, endDate);
            return Ok(new { WorkingDays = workingDays });
        }

        [HttpDelete("deleteHoliday/{id}")]
      public async Task<IActionResult> DeleteHolidayById(int id)
      {
         await _publicholidayService.DeleteHolidayAsync(id);
         return Ok();
      }

      [HttpPut("updateHoliday/{id}")]
      public async Task<IActionResult> UpdateEmployee(int id,PublicHoliday holiday)
      {
        if(id!=holiday.id)
        {
           return BadRequest("Holiday ID mismatch");
        }
        var updatedHoliday=await _publicholidayService.UpdateHolidayAsync(holiday);
        return Ok(updatedHoliday);
      }


}
