using Artezio.ART_ENGClasses.Models;
using CalendarAppointment;

namespace Artezio.ART_ENGClasses.BusinessLogic.Calendar
{
    public class SendCalendarEventArgs<T> where T : class
    {

        public SendCalendarEventArgs(T model, NotificationMethodType notificationType)
        {
            NotificationType = notificationType;
            Model = model;
        }

        public NotificationMethodType NotificationType { get; set; }
        public T Model { get; set; }
        public string Subject { get; set; }
        public AppointmentUser Organizer { get; set; }
    }
}