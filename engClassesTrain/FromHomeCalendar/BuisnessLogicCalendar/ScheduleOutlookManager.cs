using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Artezio.ART_ENGClasses.DBRepository;
using Artezio.ART_ENGClasses.Models;
using AutoMapper;
using Calendar;
using Microsoft.EntityFrameworkCore;

namespace Artezio.ART_ENGClasses.BusinessLogic.Calendar
{
    class ScheduleOutlookManager: IOutlookManager<Schedule>
    {
        //private readonly RepositoryContext _context;
        private readonly IMapper _mapper;

        public ScheduleOutlookManager(/*RepositoryContext context,*/ IMapper mapper)
        {
            //_context = context;
            _mapper = mapper;
        }
        public Schedule Add(Schedule model)
        {
            var outlookCalendar = _mapper.Map<OutlookCalendar>(model);
            CalendarCrudManager.Create(outlookCalendar);
            model.OutlookCalendar = outlookCalendar;
            return model;
        }

        public Schedule Update(Schedule model)
        {
            var id = model.OutlookCalendar.Id;
            var outlookCalendar = _mapper.Map<OutlookCalendar>(model);
            outlookCalendar.Id = id;
            outlookCalendar.Sequence = outlookCalendar.Sequence + 1;
            CalendarCrudManager.Update(outlookCalendar);
            model.OutlookCalendar = outlookCalendar;
            return model;
        }

        public Schedule Delete(Schedule model)
        {
            var id = model.OutlookCalendar.Id;
            var outlookCalendar = _mapper.Map<OutlookCalendar>(model);
            outlookCalendar.Id = id;
            CalendarCrudManager.Update(outlookCalendar);
            model.OutlookCalendar = outlookCalendar;
            return model;
        }
    }
}
