
namespace CalendarAppointment
{
    public interface IAppointmentNotificationManager
    {
        Appointment ReadAppintment(IAppointmentReader reader, object data);
        string GetICS(Appointment appointment, NotificationMethodType method);
    }
}
