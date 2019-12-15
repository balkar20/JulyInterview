using System.Runtime.InteropServices.ComTypes;
using Artezio.ART_ENGClasses.Models;

namespace Calendar
{
    public static class CalendarCrudManager
    {

        public static void Create(OutlookCalendar outlookCalendar)
        {
            CalendarSender.SendEvent(outlookCalendar, IcsResolver.ResolveIcsFromModel(outlookCalendar, NotificationMethodType.Create));
        }

        public static void Update(OutlookCalendar outlookCalendar)
        {
            CalendarSender.SendEvent(outlookCalendar, IcsResolver.ResolveIcsFromModel(outlookCalendar, NotificationMethodType.Update));
        }

        public static void Cancel(OutlookCalendar outlookCalendar)
        {
            CalendarSender.SendEvent(outlookCalendar, IcsResolver.ResolveIcsFromModel(outlookCalendar, NotificationMethodType.Delete));
        }
    }
}