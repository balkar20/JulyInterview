using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using Artezio.ART_ENGClasses.Models;

namespace Calendar
{
    public class CalendarSender
    {
        public static void SendEvent(OutlookCalendar outlookCalendar, string ics)
        {
            MailMessage mmMessage = new MailMessage();

            System.Net.Mime.ContentType typeText = new System.Net.Mime.ContentType("text/plain");
            System.Net.Mime.ContentType typeHTML = new System.Net.Mime.ContentType("text/html");
            System.Net.Mime.ContentType typeCalendar = new System.Net.Mime.ContentType("text/calendar");
            typeCalendar.Parameters.Add("name", $"{outlookCalendar.Subject}.ics");

            var strBody = new StringBuilder();
            strBody.AppendLine("Type:Event");
            strBody.AppendLine($"Organizer: {outlookCalendar.Organizer}");
            strBody.AppendLine($"Start Time:{outlookCalendar.StartDate}");
            strBody.AppendLine($"End Time:{outlookCalendar.EndDate}");
            strBody.AppendLine($"Time Zone:{TimeZoneInfo.Local.StandardName}");
            strBody.AppendLine($"Location: {outlookCalendar.Location}");
            strBody.AppendLine(outlookCalendar.Description);

            AlternateView viewText = AlternateView.CreateAlternateViewFromString(strBody.ToString(), typeText);
            mmMessage.AlternateViews.Add(viewText);
            AlternateView viewCalendar = AlternateView.CreateAlternateViewFromString(ics, typeCalendar);
            viewCalendar.TransferEncoding = TransferEncoding.SevenBit;
            mmMessage.AlternateViews.Add(viewCalendar);

            mmMessage.From = new MailAddress(EventHelper.MaiList(new List<User>(){ outlookCalendar.Organizer}).First());
            var mails = EventHelper.MaiList(outlookCalendar.Users.ToList()).Select(o => new MailAddress(o));
            foreach (MailAddress attendee in mails)
            {
                mmMessage.To.Add(attendee);
            }

            mmMessage.Subject = outlookCalendar.Subject;
            using (var smtp = new SmtpClient("smtp.artgroup.local", 9999))
            {
                smtp.EnableSsl = false;
                smtp.UseDefaultCredentials = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                smtp.Send(mmMessage);
            }
        }
    }
}