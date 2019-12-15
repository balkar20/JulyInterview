using Artezio.ART_ENGClasses.Models;
using CalendarAppointment;
using Ical.Net.CalendarComponents;

namespace Artezio.ART_ENGClasses.BusinessLogic.Calendar
{
    public class CalendarEventModel
    {
        public string ics { get; set; }
        public OutlookCalendar OutlookCalendar { get; set; }
        public string FromEmail { get; set; }
    }
}