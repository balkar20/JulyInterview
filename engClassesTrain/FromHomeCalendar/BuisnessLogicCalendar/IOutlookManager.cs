using System.Threading.Tasks;
using Artezio.ART_ENGClasses.Models;

namespace Artezio.ART_ENGClasses.BusinessLogic.Calendar
{
    public interface IOutlookManager<T>
    {
        T Add(T model);
        T Update(T model);
        T Delete(T model);
    }
}