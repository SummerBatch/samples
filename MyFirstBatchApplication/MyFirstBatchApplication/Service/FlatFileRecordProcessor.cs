using MyFirstBatchApplication.Business;
using NLog;
using Summer.Batch.Infrastructure.Item;

namespace MyFirstBatchApplication.Service
{
    /// <summary>
    /// Implements <see cref="IItemProcessor{TIn, TOut}" /> for FlatFileRecord processing duty.
    /// </summary>
    public class FlatFileRecordProcessor : IItemProcessor<FlatFileRecord, FlatFileRecord>
    {

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private const string MissingDescription = "Missing Description";

        /// <summary>
        /// Implements the business logic for FlatFielRecord processing;
        /// </summary>
        /// <param name="item">the item to process</param>
        /// <returns>the item that might have been modified by the processing</returns>
        public FlatFileRecord Process(FlatFileRecord item)
        {
            Logger.Debug("Treating item with code {0}", 
                    item !=null && item.Code !=null ?
                    item.Code.ToString():"null item or null code item"); 
            if (item != null && item.Name != null && item.Description != null && item.Name.Equals(item.Description))
            {
                Logger.Debug("Missing description for item {0} ", item.Code != null ? item.Code.ToString():"null item code");
                item.Description = MissingDescription;
            }
            return item;
        }
    }
}