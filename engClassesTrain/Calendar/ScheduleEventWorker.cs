using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Artezio.ART_ENGClasses.DBRepository;
using Artezio.ART_ENGClasses.Models;
using AutoMapper;
using CalendarAppointment;
using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Ical.Net.Serialization;

namespace Artezio.ART_ENGClasses.BusinessLogic.Calendar
{
    public class ScheduleEventWorker : IEventWorker<Schedule>
    {
        private readonly IMapper _mapper;
        private readonly RepositoryContext _dbContext;
        public CalendarEventModel ResolveEventFromModel(Schedule scheduleModel, string organizer, string subject, NotificationMethodType type)
        {
            var scheduleDt = EventHelper.GetNextWeekday(scheduleModel.From, scheduleModel.DayOfWeek);

            CalendarEvent calendarEvent = new CalendarEvent();
            Ical.Net.Calendar calendar = new Ical.Net.Calendar();
            calendar.AddTimeZone(TimeZoneInfo.Local);
            if (type == NotificationMethodType.Create)
            {
                calendarEvent.Uid = Guid.NewGuid().ToString();
                calendar.Method = "REQUEST";
            }

            if (type == NotificationMethodType.Delete)
            {
                calendar.Method = "CANCEL";
            }
            if (type == NotificationMethodType.Create)
            {
                calendarEvent.Uid = Guid.NewGuid().ToString();
                
            }
            else
            {
                calendarEvent.Uid = scheduleModel.OutlookCalendar.Id.ToString();
                calendarEvent.Sequence = scheduleModel.OutlookCalendar.Sequence + 1;
            }
            
            calendarEvent.Location = scheduleModel.Room.Name;
            calendarEvent.Organizer = new Organizer(organizer);
            calendarEvent.DtStamp = new CalDateTime(DateTime.Now);
            calendarEvent.Start = new CalDateTime(new DateTime(scheduleDt.Year, scheduleDt.Month,
                                                               scheduleDt.Day, scheduleModel.Time.Hours,
                                                          scheduleModel.Time.Minutes, scheduleModel.Time.Seconds));
            calendarEvent.End = new CalDateTime(new DateTime(scheduleDt.Year, scheduleDt.Month,
                                                             scheduleDt.Day, scheduleModel.Time.Hours,
                                                        scheduleModel.Time.Minutes, scheduleModel.Time.Seconds).AddMinutes(scheduleModel.Duration.TotalMinutes));
            var attendeeUsers = scheduleModel.Group.UserGroups.Select(ug => ug.User).ToList();
            calendarEvent.Attendees = EventHelper.GetAttendees(attendeeUsers);
            calendarEvent.RecurrenceRules = new List<RecurrencePattern> {new RecurrencePattern(FrequencyType.Weekly, 1)
                { Count = EventHelper.CountWeekDays(scheduleModel.From, scheduleModel.To, scheduleDt.DayOfWeek) }};

            calendar.Events.Add(calendarEvent);

            var serializer = new CalendarSerializer();

            var result = new CalendarEventModel()
            {
                OutlookCalendar = new OutlookCalendar()
                {
                    Id = new Guid(calendarEvent.Uid),
                    Subject = subject,
                    Location = calendarEvent.Location,
                    StartDate = calendarEvent.Start.Value,
                    EndDate = calendarEvent.End.Value,
                    Sequence = calendarEvent.Sequence,
                    // Organizer = calendarEvent.Organizer
                },
                ics = serializer.SerializeToString(calendar)
            };
            return result;
        }
    }
}