using System;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using Artezio.ART_ENGClasses.DBRepository;
using Artezio.ART_ENGClasses.Models;
using Ical.Net.CalendarComponents;

namespace Artezio.ART_ENGClasses.BusinessLogic.Calendar
{
    public class EventMailSender<T>
    {
        private readonly IEventWorker<T> eventWorker;

        public EventMailSender(IEventWorker<T> eventWorker, RepositoryContext context)
        {
            this.eventWorker = eventWorker;
        }

        public EventMailSender(IEventWorker<T> eventWorker)
        {
            this.eventWorker = eventWorker;
        }

        public OutlookCalendar SendEvent(CalendarEventModel calendarEventModel, string organizerEmail)
        {

            //var h = eventWorker.ResolveEventFromModel(calendarEventModel)
            MailMessage mmMessage = new MailMessage();

            System.Net.Mime.ContentType typeText = new System.Net.Mime.ContentType("text/plain");
            System.Net.Mime.ContentType typeHTML = new System.Net.Mime.ContentType("text/html");
            System.Net.Mime.ContentType typeCalendar = new System.Net.Mime.ContentType("text/calendar");
            typeCalendar.Parameters.Add("name", $"{calendarEventModel.OutlookCalendar.Subject}.ics");

            var strBody = new StringBuilder();
            strBody.AppendLine("Type:Event");
            strBody.AppendLine($"Organizer: {calendarEventModel.OutlookCalendar.Organizer}");
            strBody.AppendLine($"Start Time:{calendarEventModel.OutlookCalendar.StartDate}");
            strBody.AppendLine($"End Time:{calendarEventModel.OutlookCalendar.EndDate}");
            strBody.AppendLine($"Time Zone:{TimeZoneInfo.Local.StandardName}");
            strBody.AppendLine($"Location: {calendarEventModel.OutlookCalendar.Location}");
            strBody.AppendLine(calendarEventModel.OutlookCalendar.Description);

            AlternateView viewText = AlternateView.CreateAlternateViewFromString(strBody.ToString(), typeText);
            mmMessage.AlternateViews.Add(viewText);
            AlternateView viewCalendar = AlternateView.CreateAlternateViewFromString(calendarEventModel.ics, typeCalendar);
            viewCalendar.TransferEncoding = TransferEncoding.SevenBit;
            mmMessage.AlternateViews.Add(viewCalendar);

            mmMessage.From = new MailAddress(calendarEventModel.FromEmail);
            var mails = EventHelper.MaiList(calendarEventModel.OutlookCalendar.Users.ToList()).Select(m => new MailAddress(m));
            foreach (MailAddress attendee in mails)
            {
                mmMessage.To.Add(attendee);
            }

            mmMessage.Subject = calendarEventModel.OutlookCalendar.Subject;
            using (var smtp = new SmtpClient("smtp.artgroup.local", 9999))
            {
                smtp.EnableSsl = false;
                smtp.UseDefaultCredentials = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                smtp.Send(mmMessage);
            }

            return calendarEventModel.OutlookCalendar;
        }
    }
}