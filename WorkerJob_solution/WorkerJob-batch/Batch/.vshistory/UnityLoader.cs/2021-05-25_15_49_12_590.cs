using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using BA_DB_WRITER_batch.Processor;
using BA_DB_WRITER_batch.Business;
using BA_DB_WRITER_batch.Business.Mappers;
using Microsoft.Practices.Unity;
using Summer.Batch.Core.Unity;
using Summer.Batch.Data.Parameter;
using Summer.Batch.Extra;
using Summer.Batch.Infrastructure.Item;
using Summer.Batch.Infrastructure.Item.Database;
using BA_DB_WRTIER_batch;

namespace BA_DB_WRITER_batch.Batch
{
    /// <summary>
    /// Implementation of <see cref="ContextManagerUnityLoader" /> for job BA_DB_WRITER_Performance.
    /// </summary>
    public class UnityLoader : ContextManagerUnityLoader
    {
        /// <summary>
        /// Registers the artifacts required to execute the steps (tasklets, readers, writers, etc.)
        /// </summary>
        /// <param name="container">the unity container to use for registrations</param>
        public override void LoadArtifacts(IUnityContainer container)
        {
            ConnectionStringRegistration.Register(container);
            RegisterStep2(container);
        }

        /// <summary>
        /// Registers the artifacts required for step step2.
        /// </summary>
        /// <param name="container">the unity container to use for registrations</param>
        private void RegisterStep2(IUnityContainer container)
        {
            // Reader - step2/ReadEmployee
            container.StepScopeRegistration<IItemReader<EmployeeDbBO>, DataReaderItemReader<EmployeeDbBO>>("step2/ReadEmployee")
                .Property("ConnectionString").Reference<ConnectionStringSettings>("Default")
                .Property("Query").Value(SqlQueries.ReadEmployee)
                .Property("RowMapper").Instance(EmployeeDbSQLReaderMapper.RowMapper)
                .Register();

            // Processor - step2/Processor
            container.RegisterStepScope<IItemProcessor<EmployeeDbBO, EmployeeDbBO>, SqlDataExecutionListener>("step2/Processor");

            // Writer - step2/WriteEmployee
            container.StepScopeRegistration<IItemWriter<EmployeeDbBO>, DatabaseBatchItemWriter<EmployeeDbBO>>("step2/WriteEmployee")
                .Property("ConnectionString").Reference<ConnectionStringSettings>("Default")
                .Property("Query").Value(SqlQueries.WriteEmployee)
                     .Property("DbParameterSourceProvider").Reference<PropertyParameterSourceProvider<EmployeeDbBO>>()
                .Property("AssertUpdates").Value(false)
                .Register();
        }
    }
}

