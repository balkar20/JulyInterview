using System;
using System.Collections.Generic;
using System.Linq;
using Artezio.ART_ENGClasses.Models;
using Ical.Net.DataTypes;

namespace Artezio.ART_ENGClasses.BusinessLogic.Calendar
{
    public static class EventHelper
    {
        //public OutlookCalendar ResolveCalendarFromSchedule(Schedule schedule)
        //{
        //    OutlookCalendar calendar = new OutlookCalendar()
        //    {

        //    };
        //}

        public static DateTime ResolveScheduleStartTime(Schedule schedule)
        {
            var scheduleDt = EventHelper.GetNextWeekday(schedule.From, schedule.DayOfWeek);
            return new DateTime(scheduleDt.Year, scheduleDt.Month,
                scheduleDt.Day, schedule.Time.Hours,
                schedule.Time.Minutes, schedule.Time.Seconds);
        }

        public static DateTime ResolveScheduleEndTime(Schedule schedule)
        {
            var scheduleDt = EventHelper.GetNextWeekday(schedule.From, schedule.DayOfWeek);
            return new DateTime(scheduleDt.Year, scheduleDt.Month,
                scheduleDt.Day, schedule.Time.Hours,
                schedule.Time.Minutes, schedule.Time.Seconds).AddMinutes(schedule.Duration.TotalMinutes);
        }


        public static int CountWeekDays(DateTime startDate, DateTime endDate, DayOfWeek day)
        {
            int days = 0;

            for (DateTime dt = startDate; dt < endDate; dt = dt.AddDays(1.0))
            {
                if (dt.DayOfWeek == day)
                {
                    days++;
                }
            }

            return days;
        }

        public static List<Attendee> GetAttendees(List<User> users)
        {
            return MaiList(users).Select(u => new Attendee(u)).ToList();
        }

        public static List<string> MaiList(List<User> users)
        {
            var attendees = new List<string>();
            foreach (var user in users)
            {
                attendees.Add($"{user.FirstName}.{user.LastName}@artezio.com");
            }
            return attendees;
        }


        public static DateTime GetNextWeekday(DateTime start, DayOfWeek day)
        {
            // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
            int daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;
            return start.AddDays(daysToAdd);
        }


    }
}