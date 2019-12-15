using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Ical.Net.Serialization;
using Microsoft.SharePoint.Client;
using Calendar = Ical.Net.Calendar;

namespace CalendarAppointment
{
    public class AppointmentManager : IAppointmentNotificationManager
    {
        const string ICS_DATE_TIME_FORMAT = "yyyyMMddTHHmmssZ";
        const string ICS_DATE_FORMAT = "yyyyMMdd";

        public AppointmentManager()
        {

        }

        public Appointment ReadAppintment(IAppointmentReader reader, object data)
        {
            return reader.Read(data);
        }

        public string GetIcsViaLib(CalendarEvent calendarEvent, NotificationMethodType actionType)
        {
            var calendar = new Calendar();
            
            calendar.Events.Add(calendarEvent);
            if (actionType == NotificationMethodType.Create )
            {
                calendar.Method = "REQUEST";
            }
            
            calendar.AddTimeZone(TimeZoneInfo.Local);
            if (actionType == NotificationMethodType.Delete)
            {
                calendar.Method = "CANCEL";
            }

            var serializer = new CalendarSerializer();
            var serializedCalendar = serializer.SerializeToString(calendar);
            return serializedCalendar;
        }

        private int CountWeekDays(DateTime startDate, DateTime endDate, DayOfWeek day)
        {
            int days = 0;

            for (DateTime dt = startDate; dt < endDate; dt = dt.AddDays(1.0))
            {
                if (dt.DayOfWeek == day)
                {
                    days++;
                }
            }

            return days;
        }

        public void SendAppointments(Appointment appointment, NotificationMethodType actionType)
        {
            MailMessage mmMessage = new MailMessage();

            System.Net.Mime.ContentType typeText = new System.Net.Mime.ContentType("text/plain");
            System.Net.Mime.ContentType typeHTML = new System.Net.Mime.ContentType("text/html");
            System.Net.Mime.ContentType typeCalendar = new System.Net.Mime.ContentType("text/calendar");

            typeCalendar.Parameters.Add("name", $"{appointment.Subject}.ics");
            if (actionType == NotificationMethodType.Create)
            {
                typeCalendar.Parameters.Add("method", "REQUEST");
            }
            else if (actionType == NotificationMethodType.Delete)
            {
                typeCalendar.Parameters.Add("method", "CANCEL");
            }
            var strBody = new StringBuilder();
            strBody.AppendLine("Type:Event");
            strBody.AppendLine($"Organizer: {appointment.Organizer.DisplayName}");
            strBody.AppendLine($"Start Time:{appointment.StartDate}");
            strBody.AppendLine($"End Time:{appointment.EndDate}");
            strBody.AppendLine($"Time Zone:{TimeZoneInfo.Local.StandardName}");
            strBody.AppendLine($"Location: {appointment.Location}");
            strBody.AppendLine(appointment.Description);

            AlternateView viewText = AlternateView.CreateAlternateViewFromString(strBody.ToString(), typeText);
            mmMessage.AlternateViews.Add(viewText);

            //AlternateView viewCalendar = AlternateView.CreateAlternateViewFromString(GetICS(appointment, actionType), typeCalendar);
            //AlternateView viewCalendar = AlternateView.CreateAlternateViewFromString(GetIcsViaLib(appointment, actionType), typeCalendar);
            //viewCalendar.TransferEncoding = TransferEncoding.SevenBit;
            //mmMessage.AlternateViews.Add(viewCalendar);

            mmMessage.From = new MailAddress(appointment.Organizer.Email);
            var mails = appointment.Attendees.Select(a => new MailAddress(a.Email));
            foreach (MailAddress attendee in mails)
            {
                mmMessage.To.Add(attendee);
            }
            mmMessage.Subject = appointment.Subject;
            using (var smtp = new SmtpClient("smtp.artgroup.local", 9999))
            {
                smtp.EnableSsl = false;
                smtp.UseDefaultCredentials = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                smtp.Send(mmMessage);
            }
        }

        public string GetICS(Appointment appointment, NotificationMethodType method)
        {
            string methodIcs = "REQUEST";
            string status = "CONFIRMED";
            int sequence = appointment.Sequence;
            switch (method)
            {
                case NotificationMethodType.Create:
                    break;
                case NotificationMethodType.Update:
                    status = "TENTATIVE";
                    sequence = appointment.Sequence + 1;
                    break;
                case NotificationMethodType.Delete:
                    status = "CANCELLED";
                    methodIcs = "CANCEL";
                    break;
                default:
                    break;
            }

            var tzid = TimeZoneInfo.Local.DisplayName;
            List<string> contents = new List<string>();
            
            contents.AddRange(new string[] {
                                  "BEGIN:VCALENDAR",
                                  "METHOD:" + methodIcs,
                                  "PRODID:PRODID:Microsoft CDO for Microsoft Exchange",
                                  "VERSION:2.0",
                                  "BEGIN:VTIMEZONE",
                                  $"TZID:Europe/Minsk",
                                  //"LAST-MODIFIED:20191023T183904Z",
                                  "X-LIC-LOCATION:Europe/Minsk",
                                  "BEGIN:STANDARD",
                                  "TZNAME:+03",
                                  "DTSTART:19700101T000000",
                                  "TZOFFSETFROM:+0000",
                                  "TZOFFSETTO:+0000",
                                  //"RRULE:FREQ=YEARLY;WKST=MO;INTERVAL=1;BYMONTH=11;BYDAY=1SU",
                                  "END:STANDARD",
                                  "BEGIN:DAYLIGHT",
                                  "DTSTART:19700101T000000",
                                  "TZOFFSETFROM:+0000",
                                  "TZOFFSETTO:+0000",
                                  "RRULE:FREQ=YEARLY;WKST=MO;INTERVAL=1;BYMONTH=3;BYDAY=2SU",
                                  "END:DAYLIGHT",
                                  "END:VTIMEZONE",
                                  "BEGIN:VEVENT",
                                  "DTSTAMP:" + appointment.StampDate.ToUniversalTime().ToString(ICS_DATE_TIME_FORMAT),
            });
            if (appointment.AllDayEvent)
            {
                contents.AddRange(new string[] {
                                  "DTSTART;TZID=Europe/Moscow:" + appointment.StartDate.ToUniversalTime().ToString(ICS_DATE_FORMAT),
                                  "DTEND;TZID=Europe/Moscow:" + appointment.EndDate.ToUniversalTime().ToString(ICS_DATE_FORMAT) });
            }
            else
            {
                contents.AddRange(new string[] {
                                  "DTSTART:" + appointment.StartDate.ToUniversalTime().ToString(ICS_DATE_TIME_FORMAT),
                                  "DTEND:" + appointment.EndDate.ToUniversalTime().ToString(ICS_DATE_TIME_FORMAT) });
            }

            contents.AddRange(new string[] {
                                  "UID:" + appointment.Id,
                                  "PRIORITY:3",
                                  "LOCATION:" + appointment.Location,
                                  "STATUS:" + status,
                                  "DESCRIPTION;ENCODING=QUOTED-PRINTABLE:" + HtlmXmlHelper.StripXml(appointment.Description)});
                                  //"X-ALT-DESC;FMTTYPE=text/html:" + HtlmXmlHelper.GetHtmlDocument(appointment.Description)});

            if (appointment.Organizer != null)
            {
                contents.Add(string.Format("ORGANIZER;CN={0}:MAILTO:{1}", appointment.Organizer.DisplayName, appointment.Organizer.Email));
            }

            foreach (AppointmentUser a in appointment.Attendees)
            {
                contents.Add(string.Format("ATTENDEE;CUTYPE=INDIVIDUAL;ROLE=REQ-PARTICIPANT;PARTSTAT=NEEDS-ACTION;RSVP=FALSE;CN={0};X-NUM-GUESTS=0:mailto:{1}", a.DisplayName, a.Email));
            }

            if (appointment.Recurrence)
                contents.Add(GetRRULE(appointment.RecurrenceData));


            contents.AddRange(new string[] {
                                  "SEQUENCE:" + sequence.ToString(),
                                  "X-MICROSOFT-CDO-APPT-SEQUENCE:" + sequence.ToString(),
                                  "X-MICROSOFT-CDO-BUSYSTATUS:TENTATIVE",
                                  "X-MICROSOFT-CDO-INTENDEDSTATUS:BUSY",
                                  "X-MICROSOFT-CDO-ALLDAYEVENT:" + appointment.AllDayEvent.ToString().ToUpper(),
                                  "X-MICROSOFT-CDO-IMPORTANCE:1",
                                  "X-MICROSOFT-CDO-INSTTYPE:0",
                                  "X-MICROSOFT-DISALLOW-COUNTER:FALSE",
                                  "SUMMARY:" + appointment.Subject,
                                  "END:VEVENT",
                                  "END:VCALENDAR" });


            //var result = string.Join("\r\n", contents.ToArray());
            string strBody = $"BEGIN:VCALENDAR\r\nMETHOD:REQUEST\r\nPRODID:Microsoft CDO for Microsoft Exchange\r\nVERSION:2.0\r\nBEGIN:VTIMEZONE\r\nTZID:(GMT-06.00) Central Time (US &amp; Canada)\r\nX-MICROSOFT-CDO-TZID:11\r\nBEGIN:STANDARD\r\nDTSTART:16010101T020000\r\nTZOFFSETFROM:-0500\r\nTZOFFSETTO:-0600\r\nRRULE:FREQ=YEARLY;WKST=MO;INTERVAL=1;BYMONTH=11;BYDAY=1SU\r\nEND:STANDARD\r\nBEGIN:DAYLIGHT\r\nDTSTART:16010101T020000\r\nTZOFFSETFROM:-0600\r\nTZOFFSETTO:-0500\r\nRRULE:FREQ=YEARLY;WKST=MO;INTERVAL=1;BYMONTH=3;BYDAY=2SU\r\nEND:DAYLIGHT\r\nEND:VTIMEZONE\r\nBEGIN:VEVENT\r\nDTSTAMP:{DateTime.Now.ToUniversalTime().ToString(ICS_DATE_TIME_FORMAT)}\r\nDTSTART:{appointment.StartDate.ToUniversalTime().ToString(ICS_DATE_TIME_FORMAT)}\r\nSUMMARY:{appointment.Subject}\r\n" +
                                     $"UID:{appointment.Id}\r\nATTENDEE;ROLE=REQ-PARTICIPANT;PARTSTAT=NEEDS-ACTION;RSVP=TRUE;CN=\"{appointment.Attendees[0].Email}\":MAILTO:{appointment.Attendees[0].Email}\r\nACTION;RSVP=TRUE;CN=\"{appointment.Organizer.Email}\":MAILTO:{appointment.Organizer.Email}\r\nORGANIZER;CN=\"{appointment.Organizer.DisplayName}\":mailto:{appointment.Organizer.Email}\r\nLOCATION:{appointment.Location}\r\nDTEND:{appointment.EndDate.ToUniversalTime().ToString(ICS_DATE_TIME_FORMAT)}\r\nDESCRIPTION:{appointment.Subject}\\N\r\nSEQUENCE:1\r\nPRIORITY:5\r\nCLASS:\r\nCREATED:{ DateTime.Now.ToUniversalTime().ToString(ICS_DATE_TIME_FORMAT)}\r\nLAST-MODIFIED:{ DateTime.Now.ToUniversalTime().ToString(ICS_DATE_TIME_FORMAT)}\r\n" +
                                     $"STATUS:CONFIRMED\r\nTRANSP:OPAQUE\r\nX-MICROSOFT-CDO-BUSYSTATUS:BUSY\r\nX-MICROSOFT-CDO-INSTTYPE:0\r\nX-MICROSOFT-CDO-INTENDEDSTATUS:BUSY\r\nX-MICROSOFT-CDO-ALLDAYEVENT:FALSE\r\nX-MICROSOFT-CDO-IMPORTANCE:1\r\nX-MICROSOFT-CDO-OWNERAPPTID:-1\r\nX-MICROSOFT-CDO-ATTENDEE-CRITICAL-CHANGE:{DateTime.Now.ToUniversalTime().ToString(ICS_DATE_TIME_FORMAT)}\r\nX-MICROSOFT-CDO-OWNER-CRITICAL-CHANGE:{DateTime.Now.ToUniversalTime().ToString(ICS_DATE_TIME_FORMAT)}\r\nBEGIN:VALARM\r\nACTION:DISPLAY\r\nDESCRIPTION:REMINDER\r\nTRIGGER;RELATED=START:-PT00H15M00S\r\nEND:VALARM\r\nEND:VEVENT\r\nEND:VCALENDAR\r\n";
            var returningStr = string.Join("\r\n", contents.ToArray());
            return string.Join("\r\n", contents.ToArray());
            //return strBody;
        }

        private string GetRRULE(AppointmentRecurrence recurrence)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("RRULE:");
            sb.Append("FREQ=");
            sb.Append(recurrence.Type.ToString());
            if (recurrence.UntilDate != null)
            {
                sb.Append(";UNTIL=");
                sb.Append(recurrence.UntilDate.Value.ToUniversalTime().ToString(ICS_DATE_TIME_FORMAT));
            }

            if (recurrence.RepeatInstances > 0)
            {
                sb.Append(";COUNT=");
                sb.Append(recurrence.RepeatInstances.ToString());
            }

            if (recurrence.Frequency > 0)
            {
                sb.Append(";INTERVAL=");
                sb.Append(recurrence.Frequency);
            }

            if (recurrence.MonthDay > 0)
            {
                sb.Append(";BYMONTHDAY=");
                sb.Append(recurrence.MonthDay.ToString());
            }

            if (recurrence.Month > 0)
            {
                sb.Append(";BYMONTH=");
                sb.Append(recurrence.Month.ToString());
            }

            switch (recurrence.Type)
            {
                case RecuringType.DAILY:
                    break;
                case RecuringType.WEEKLY:
                    if (recurrence.Days.Count > 0)
                    {
                        sb.Append(";BYDAY=");
                        var day = string.Join(",", recurrence.Days.Select(d => d.ToString()).ToArray());
                        sb.Append(string.Join(",", recurrence.Days.Select(d => d.ToString()).ToArray()));
                    }
                    break;
                case RecuringType.MONTHLY:
                    break;
                case RecuringType.YEARLY:
                    break;
                default:
                    break;
            }

            sb.Append(";WKST=");
            sb.Append(recurrence.WeekStart.ToString());

            return sb.ToString();
        }
    }
}