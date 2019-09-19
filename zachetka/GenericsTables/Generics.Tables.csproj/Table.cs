using System;
using System.Collections.Generic;
using System.Linq;

namespace Generics.Tables
{
    public class Table<TRow, TColumn, TVal> where TRow : 
        IComparable where TColumn : 
        IComparable where TVal : 
        IComparable
    {
        public Table()
        {
            Data = new List<Data<TRow, TColumn, TVal>>();
            Rows = new List<TRow>();
            Columns = new List<TColumn>();
            Open = new TableWorker<TRow, TColumn, TVal>(this, Data, OpenExists.Open);
            Existed = new TableWorker<TRow, TColumn, TVal>(this, Data, OpenExists.Exists);
        }

        public List<TRow> Rows { get; set; }
        public List<TColumn> Columns { get; set; }
        private List< Data<TRow, TColumn, TVal>> Data { get; }
        public TableWorker<TRow, TColumn, TVal> Open { get; set; }
        public TableWorker<TRow, TColumn, TVal> Existed { get; set; }

        public void AddRow(TRow row)
        {
            if (!Rows.Contains(row))
            {
                Rows.Add(row);
            }
        }

        public void AddColumn(TColumn column)
        {
            if (!Columns.Contains(column))
            {
                Columns.Add(column);
            }
        }
    }

    public struct Data<TRow, TColumn, TVal>
    {
        public Data(TRow row, TColumn column, TVal value)
        {
            Row = row;
            Column = column;
            Value = value;
        }

        public TRow Row { get; set; }
        public TColumn Column { get; set; }
        public TVal Value { get; set; }
    }

    public enum OpenExists
    {
        Open,
        Exists
    }

    public class TableWorker<TRow, TColumn, TVal> where TRow : 
        IComparable where TColumn : 
        IComparable where TVal : 
        IComparable
    {
        public TableWorker(Table<TRow, TColumn, TVal> table, 
            List<Data<TRow, TColumn, TVal>> data, OpenExists openExists)
        {
            OpenExists = openExists;
            Table = table;
            Data = data;
        }
        
        public List<Data<TRow, TColumn, TVal>> Data { get; set; }
        private Table<TRow, TColumn, TVal> Table { get; }
        private OpenExists OpenExists { get; }

        public TVal this[TRow row, TColumn column]
        {
            get
            {
                try
                {
                    var data = Data.Where(d => d.Row.CompareTo(row) == 0 &&
                                                               d.Column.CompareTo(column) == 0).FirstOrDefault();
                    if (OpenExists == OpenExists.Exists)
                    {
                        if (!Table.Rows.Contains(row) || 
                            !Table.Columns.Contains(column))
                        {
                            throw new ArgumentException();
                        }
                    }
                    return data.Value;
                }
                catch (InvalidOperationException)
                {
                    throw new ArgumentException();
                }
            }
            set
            {
                AddRowsorColumnsIfNotContains(row, column);
                Data.Add(new Data<TRow, TColumn, TVal>(row, column, value));
            }
        }

        private void AddRowsorColumnsIfNotContains(TRow row, TColumn column)
        {
            if (!Table.Rows.Contains(row) &&
                !Table.Columns.Contains(column))
            {
                Table.Rows.Add(row);
                Table.Columns.Add(column);
            }
            else if ((!Table.Rows.Contains(row) &&
                     Table.Columns.Contains(column)) ||
                     (Table.Rows.Contains(row) &&
                     !Table.Columns.Contains(column)))
            {
                throw new ArgumentException();
            }
        }
    }
}
