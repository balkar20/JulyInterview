using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CalendarAppointment
{
    public static class HtlmXmlHelper
    {
        private static Regex regexHTML = new Regex("<.*?>", RegexOptions.Compiled);
        private static Regex regexXML = new Regex("&.*?;", RegexOptions.Compiled);


        public static string StripXml(string xmlString)
        {
            return new string(regexXML.Replace(regexHTML.Replace(xmlString, string.Empty), string.Empty).Where(c => c != 8203).ToArray());
        }

        public static string GetHtmlDocument(Appointment appointment)
        {
            return $"<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 3.2//EN\">\r\n<HTML>\r\n" +
                      $"<HEAD>\r\n<META HTTP-EQUIV=\"Content-Type\" CONTENT=\"text/html; charset=utf-8\">\r\n" +
                      $"<META NAME=\"Generator\" CONTENT=\"MS Exchange Server version 6.5.7652.24\">\r\n" +
                      $"<TITLE>{appointment.Subject}</TITLE>\r\n" +
                      $"</HEAD>\r\n<BODY>\r\n<!-- Converted from text/plain format -->\r\n<P>" +
                      $"<FONT SIZE=2>Type:Single Meeting<BR>\r\n" +
                      $"Organizer:{appointment.Organizer.Email}<BR>\r\n" +
                      $"Start Time:{appointment.StartDate}<BR>\r\n" +
                      $"End Time:{appointment.EndDate}<BR>\r\n" +
                      $"Time Zone:{TimeZoneInfo.Local.StandardName}<BR>\r\n" +
                      $"Location:{appointment.Location}<BR>\r\n<BR>\r\n*~*~*~*~*~*~*~*~*~*<BR>\r\n<BR>\r\n" +
                      $"{appointment.Description}<BR>\r\n</FONT>\r\n</P>\r\n\r\n</BODY>\r\n</HTML>";
        }
    }
}