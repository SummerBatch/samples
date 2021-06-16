using BA_DB_WRITER_batch.Business;
using BA_DB_WRITER_batch.Services;
using Microsoft.Practices.Unity;
using Summer.Batch.Extra;
using Summer.Batch.Extra.Service.Stop;
using Summer.Batch.Infrastructure.Item;

namespace BA_DB_WRITER_batch.Processor
{
    public class SqlDataExecutionListener : AbstractExecutionListener, IItemProcessor<EmployeeDbBO, EmployeeDbBO>
    {
        /// <summary>
        /// Service that implements the processor method.
        /// </summary>
        [Dependency]
        public ServiceWriteSql Processor { get; set; }

        /// <summary>
        /// Processes the read items to provide them to the writer.
        /// </summary>
        /// <param name="item">The item to process.</param>
        /// <returns>The <see cref="EmployeeDbBO" /> to write, or null if <paramref cref="item" /> should be skipped.</returns>
        public EmployeeDbBO Process(EmployeeDbBO item)
        {
            try
            {
                return Processor.Processor(item);
            }
            catch (SkippedItemException)
            {
                // item is skipped, just return null
                return null;
            }
        }
    }
}
