using System.Threading.Tasks;
using Artezio.ART_ENGClasses.Models;
using CalendarAppointment;
using Ical.Net.CalendarComponents;

namespace Artezio.ART_ENGClasses.BusinessLogic.Calendar
{
    public interface IEventWorker<T>
    {
        CalendarEventModel ResolveEventFromModel(T classModel, string organizer, string subject, NotificationMethodType type);
    }
}