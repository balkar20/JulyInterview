using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Artezio.ART_ENGClasses.DBRepository;
using Artezio.ART_ENGClasses.Models;
using CalendarAppointment;

namespace Artezio.ART_ENGClasses.BusinessLogic.Calendar
{
    public abstract class OutlookManager<T> where T : class
    {
        private readonly EventMailSender<T> mailSender;
        private readonly RepositoryContext _context;
        private readonly IEventWorker<T> eventWorker;

        public OutlookManager(EventMailSender<T> mailSender, RepositoryContext context, IEventWorker<T> eventWorker)
        {
            this.mailSender = mailSender;
            _context = context;
            this.eventWorker = eventWorker;
        }

        public async Task DoEventWork(T model, string organizer, string subject, NotificationMethodType notificationMethod, string fromEmail)
        {
            
            OutlookCalendar calendar = mailSender.SendEvent(eventWorker.ResolveEventFromModel(model, organizer, subject, notificationMethod),
                fromEmail);
            await _context.OutlookCalendars.AddAsync(calendar);
            await _context.SaveChangesAsync();
        }
        public async Task AddEvent(OutlookCalendar calendar, NotificationMethodType methodType)
        {
            
        }
    }
}
