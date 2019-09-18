using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generics.Tables
{
    public class Table<TRow, TColumn, TVal> 
    {
        public TRow[] Rows { get; set; }
        public TColumn[] Columns { get; set; }

        public TableWorker<TRow, TColumn, TVal> Open { get; set; }
        public TableWorker<TRow, TColumn, TVal> Existed { get; set; }

        

        public void AddRow(TRow row)
        {

        }

        public void AddColumn(TColumn column)
        {

        }

        public TVal GetData(TRow row, TColumn column)
        {
            throw new NotImplementedException();
        }
    }

    public class TableWorker<TR, TC, TVal>
    {
        public Table<TR, TC, TVal> Table { get; set; }

        public TVal this[TR row, TC column]
        {
            get
            {
                return Table.GetData(row, column);
            }
            set
            {
                
            }
        }
    }
}
