using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Artezio.ART_ENGClasses.DBRepository;
using Artezio.ART_ENGClasses.Models;
using Calendar;

namespace Artezio.ART_ENGClasses.BusinessLogic.Calendar
{
    public abstract class ClassOutlookManager: IOutlookManager<Class>
    {
        private readonly RepositoryContext _context;

        public ClassOutlookManager(RepositoryContext context)
        {
          _context = context;

        }


        public Class Add(Class model)
        {
            //CalendarCrudManager.Create(outlookCalendar);
            //_context.
            throw new NotImplementedException();
        }

        public Class Update(Class model)
        {
            throw new NotImplementedException();
        }

        public Class Delete(Class model)
        {
            throw new NotImplementedException();
        }
    }
}
