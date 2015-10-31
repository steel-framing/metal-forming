using System.Data;
using OpenSource.Library.DataAccess;

namespace MetalForming.Data.Core
{
    public class BaseData
    {
        private readonly DbContext _context;

        protected BaseData()
        {
            _context = new DbContext();
        }

        protected BaseData(string dbname)
        {
            _context = new DbContext(dbname);
        }

        protected DbContext Context
        {
            get { return _context; }
        }

        protected T GetDataValue<T>(IDataReader dr, string columnName)
        {
            var i = dr.GetOrdinal(columnName);

            if (!dr.IsDBNull(i))
                return (T)dr.GetValue(i);
            return default(T);
        }

        protected T GetDataValue<T>(object[] values, int posCol)
        {
            if (values.Length > posCol && values[posCol] != null)
                return (T)values[posCol];
            return default(T);
        }
    }
}
