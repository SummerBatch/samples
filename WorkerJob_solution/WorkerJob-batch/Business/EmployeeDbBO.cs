using System;
using System.Collections.Generic;
using System.Text;

namespace BA_DB_WRITER_batch.Business
{
    /// <summary>
    /// Entity EmployeeDbBO.
    /// </summary>
    [Serializable]
    public class EmployeeDbBO
    {
        /// <summary>
        /// Property EmpId.
        /// </summary>
        public int? EmpId { get; set; }

        /// <summary>
        /// Property EmpName.
        /// </summary>
        public string EmpName { get; set; }

        /// <summary>
        /// Property CompanyId.
        /// </summary>
        public long? CompanyId { get; set; }

        /// <summary>
        /// Property EmpSalary.
        /// </summary>
        public decimal? EmpSalary { get; set; }
    }
}
