using System;
using WowCharComparerWebApp.Models.Abstract;

namespace WowCharComparerWebApp.Models.DataTransferObject
{
    public class DbOperationStatus<T> where T: DatabaseTableModel
    {
        public DbOperationStatus()
        {
            TableModelType = typeof(T);
        }

        public object QueryResult { get; set; }

        public bool OperationSuccess { get; set; }

        public int RowsAffected { get; set; }

        public Type TableModelType { get; private set; }

        public Exception DbOperationException { get;  set; }

    }
}
