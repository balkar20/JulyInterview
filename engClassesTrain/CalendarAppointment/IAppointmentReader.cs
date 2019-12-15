﻿namespace CalendarAppointment
{
    public interface IAppointmentReader
    {
        Appointment Read(object data);
        AppointmentRecurrence ReadRecurrenceData(string recurrenceXml);
    }
}