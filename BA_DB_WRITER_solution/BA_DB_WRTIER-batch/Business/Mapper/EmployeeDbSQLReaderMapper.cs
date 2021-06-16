using System;
using System.Collections.Generic;
using System.Text;
using Summer.Batch.Data;

namespace BA_DB_WRITER_batch.Business.Mappers
{
    /// <summary>
    /// Utility class defining a row mapper for SQL readers.
    /// </summary>
    public static class EmployeeDbSQLReaderMapper
    {
        /// <summary>
        /// Row mapper for <see cref="EmployeeDbBO" />.
        /// </summary>
        public static readonly RowMapper<EmployeeDbBO> RowMapper = (dataRecord, rowNumber) =>
        {
            var wrapper = new DataRecordWrapper(dataRecord);
            return new EmployeeDbBO
            {
                EmpId = wrapper.Get<int?>(0),
                EmpName = wrapper.Get<string>(1),
                CompanyId = wrapper.Get<long?>(2),
                EmpSalary = wrapper.Get<decimal?>(3),
            };
        };
    }
}
