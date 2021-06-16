using System;
using System.Collections.Generic;
using System.Text;
using BA_DB_WRITER_batch.Business;
using NLog;
using Summer.Batch.Extra.Service;

namespace BA_DB_WRITER_batch.Services
{
    public class ServiceWriteSql : AbstractService
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Process operation Processor.
        /// </summary>
        /// <param name="employeeDb"></param>
        /// <returns></returns>
        public EmployeeDbBO Processor(EmployeeDbBO employeeDb)
        {
            Logger.Trace("ServiceWriteSql.Processor - Start");
           
            Logger.Info("Recording copy of employee with id ={0}", employeeDb.EmpId);

            Logger.Trace("ServiceWriteSql.Processor - End");

            return employeeDb;
        }
    }
}
