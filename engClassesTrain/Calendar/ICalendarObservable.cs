using System;

namespace Artezio.ART_ENGClasses.BusinessLogic.Calendar
{
    public interface ICalendarObservable<T> where T: class
    {
        event EventHandler<SendCalendarEventArgs<T>> CalendarOperation;
    }
}