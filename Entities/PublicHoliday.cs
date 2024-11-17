using System;

namespace Waterlily.Api.Entities;

public class PublicHoliday
{
public int id { get; set; }
public DateOnly HolidayDate {get;set;}
public string Reason {get;set;}="";

}
