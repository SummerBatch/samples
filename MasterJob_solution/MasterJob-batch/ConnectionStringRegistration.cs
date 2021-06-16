using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using Summer.Batch.Common.Settings;
using Summer.Batch.Data;
using Microsoft.Practices.Unity;
using Summer.Batch.Core.Unity;

namespace BA_DB_WRITER_batch
{
    public static class ConnectionStringRegistration
    {
        ///<summary>
        /// Registering the Connection String into the given Unity container.
        ///</summary>
        ///<param name="container">target unity container</param>	
        public static void Register(IUnityContainer container)
        {
            var settingsManager = container.Resolve<SettingsManager>();
            settingsManager.ConfigurationFile = "ConnectionStrings.config";

            // Default Connection
            ConnectionStringSettings connectionStringDefault = new ConnectionStringSettings("Default", settingsManager.GetConnectionString("Default"), settingsManager.GetProviderName("Default"));
            container.RegisterInstance<ConnectionStringSettings>("Default", connectionStringDefault);

            container.SingletonRegistration<IConnectionProvider, ConnectionProvider>("DefaultConnectionProvider")
                .Property("ConnectionStringSettings").Reference<ConnectionStringSettings>("Default")
                .Register();

            container.SingletonRegistration<DbOperator>("DefaultDbOperator")
                .Property("ConnectionProvider").Reference<IConnectionProvider>("DefaultConnectionProvider")
                .Register();

            container.SingletonRegistration<DataQueue>("DataQueue")
             .Property("HostName").Value("localhost")
             .Property("QueueName").Value("data")
             .Register();

        }

    }
}

