using System;
using System.Collections.Generic;
using System.Linq;
using Artezio.ART_ENGClasses.Models;
using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Ical.Net.Serialization;

namespace Calendar
{
    public class IcsResolver
    {
        public static string ResolveIcsFromModel(OutlookCalendar outlookCalendar, NotificationMethodType methodType)
        {
            CalendarEvent calendarEvent = new CalendarEvent();
            Ical.Net.Calendar calendar = new Ical.Net.Calendar();
            calendar.AddTimeZone(TimeZoneInfo.Local);
            calendar.Method = "REQUEST";
            if (methodType == NotificationMethodType.Create)
            {
                calendarEvent.Uid = Guid.NewGuid().ToString();
            }
            if (methodType == NotificationMethodType.Delete)
            {
                calendar.Method = "CANCEL";
            }
            if(methodType == NotificationMethodType.Update)
            {
                calendarEvent.Uid = outlookCalendar.Id.ToString();
                calendarEvent.RecurrenceId = new CalDateTime((outlookCalendar.OldDate));
                calendarEvent.Sequence = outlookCalendar.Sequence + 1;
            }
            calendarEvent.Location = outlookCalendar.Location;
            calendarEvent.Organizer = new Organizer($"{outlookCalendar.Organizer.FirstName} {outlookCalendar.Organizer.LastName}") { };
            calendarEvent.Start = new CalDateTime(outlookCalendar.StartDate);
            calendarEvent.End = new CalDateTime(outlookCalendar.EndDate);
            if (outlookCalendar.RecurrenceData != null)
            {
                calendarEvent.RecurrenceRules = new List<RecurrencePattern> {new RecurrencePattern(FrequencyType.Weekly, 1)
                { Count = EventHelper.CountWeekDays(outlookCalendar.RecurrenceData.From, outlookCalendar.RecurrenceData.To, outlookCalendar.RecurrenceData.DayOfWeek) }};
            }
            
            var attendeeUsers = outlookCalendar.Users.ToList();
            calendarEvent.Attendees = EventHelper.GetAttendees(attendeeUsers);
            calendar.Events.Add(calendarEvent);

            var serializer = new CalendarSerializer();

            var ics = serializer.SerializeToString(calendar);
            return ics;
        }
    }
}
