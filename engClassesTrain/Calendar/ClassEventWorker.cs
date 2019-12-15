using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using Artezio.ART_ENGClasses.BusinessLogic.AutoMapper;
using Artezio.ART_ENGClasses.DBRepository;
using Artezio.ART_ENGClasses.Dtos;
using Artezio.ART_ENGClasses.Models;
using AutoMapper;
using CalendarAppointment;
using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Ical.Net.Serialization;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.Expressions.Internal;

namespace Artezio.ART_ENGClasses.BusinessLogic.Calendar
{
    public class ClassEventWorker: IEventWorker<Class>
    {
        private readonly IMapper _mapper;
        private readonly RepositoryContext _dbContext;

        public ClassEventWorker(IMapper mapper, RepositoryContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public CalendarEventModel ResolveEventFromModel(Class classModel, string organizer, string subject, NotificationMethodType type)
        {
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
            else
            {
                calendarEvent.Uid = classModel.OutlookCalendar.Id.ToString();
                calendarEvent.RecurrenceId = new CalDateTime((classModel.OldDate.Value));
                calendarEvent.Sequence = classModel.OutlookCalendar.Sequence + 1;
            }
            calendarEvent.Location = classModel.Schedule.Room.Name;
            calendarEvent.Organizer = new Organizer(organizer){};
            calendarEvent.Start = new CalDateTime(classModel.Date);
            calendarEvent.End = new CalDateTime(classModel.Date.AddMinutes(classModel.Duration.TotalMinutes));

            var attendeeUsers = classModel.Group.UserGroups.Select(ug => ug.User).ToList();
            calendarEvent.Attendees = EventHelper.GetAttendees(attendeeUsers);
            calendar.Events.Add(calendarEvent);

            var serializer = new CalendarSerializer();
            var result =  new CalendarEventModel()
            {
                OutlookCalendar = new OutlookCalendar()
                {
                    Id = new Guid(calendarEvent.Uid),
                    Subject = subject,
                    Location = calendarEvent.Location,
                    StartDate = calendarEvent.Start.Value,
                    EndDate = calendarEvent.End.Value,
                    Sequence = calendarEvent.Sequence,
                    Organizer = calendarEvent.Organizer.Value.OriginalString,
                    Users = attendeeUsers
                },
                ics = serializer.SerializeToString(calendar)
            };
            return result;
        }
    }
}