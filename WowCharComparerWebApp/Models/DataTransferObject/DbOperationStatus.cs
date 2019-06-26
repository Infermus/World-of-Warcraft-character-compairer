using System;

namespace WowCharComparerWebApp.Models.DataTransferObject
{
    public class DbOperationStatus
    {
        public DbOperationStatus(Type databaseTableModel)
        {
            TableModelType = databaseTableModel;
        }

        public bool OperationSuccess { get; set; }

        public int RowsAffected { get; set; }

        public Type TableModelType { get; private set; }

        public Exception DbOperationException { get;  set; }
    }
}
