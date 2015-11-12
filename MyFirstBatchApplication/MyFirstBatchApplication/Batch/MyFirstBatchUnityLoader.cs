using System.Configuration;
using System.Text;
using Microsoft.Practices.Unity;
using MyFirstBatchApplication.Business;
using MyFirstBatchApplication.Business.Mappers;
using MyFirstBatchApplication.Service;
using Summer.Batch.Common.IO;
using Summer.Batch.Core.Unity;
using Summer.Batch.Data.Parameter;
using Summer.Batch.Infrastructure.Item;
using Summer.Batch.Infrastructure.Item.Database;
using Summer.Batch.Infrastructure.Item.File;
using Summer.Batch.Infrastructure.Item.File.Mapping;
using Summer.Batch.Infrastructure.Item.File.Transform;

namespace MyFirstBatchApplication.Batch
{
    /// <summary>
    /// Batch unity configuration 
    /// </summary>
    public class MyFirstBatchUnityLoader : UnityLoader
    {
        /// <summary>
        /// Registers the artifacts required to execute the steps (tasklets, readers, writers, etc.)
        /// </summary>
        /// <param name="container">the unity container to use for registrations</param>
        public override void LoadArtifacts(IUnityContainer container)
        {
            //Connection string
            var writerConnectionstring = ConfigurationManager.ConnectionStrings["Default"];

            //input file
            var inputFileResource = new FileSystemResource("data/input/LargeFlatFile.txt");

            // Reader - FlatFileReader/FlatFileReader
            container.StepScopeRegistration<IItemReader<FlatFileRecord>, FlatFileItemReader<FlatFileRecord>>("FlatFileReader/FlatFileReader")
                .Property("Resource").Value(inputFileResource)
                .Property("Encoding").Value(Encoding.GetEncoding("UTF-8"))
                .Property("LineMapper").Reference<ILineMapper<FlatFileRecord>>("FlatFileReader/FlatFileReader/LineMapper")
                .Register();

            // Line mapper
            container.StepScopeRegistration<ILineMapper<FlatFileRecord>, DefaultLineMapper<FlatFileRecord>>("FlatFileReader/FlatFileReader/LineMapper")
                .Property("Tokenizer").Reference<ILineTokenizer>("FlatFileReader/FlatFileReader/Tokenizer")
                .Property("FieldSetMapper").Reference<IFieldSetMapper<FlatFileRecord>>("FlatFileReader/FlatFileReader/FieldSetMapper")
                .Register();

            // Tokenizer
            container.StepScopeRegistration<ILineTokenizer, DelimitedLineTokenizer>("FlatFileReader/FlatFileReader/Tokenizer")
                .Property("Delimiter").Value(";")
                .Register();

            // Field set mapper
            container.RegisterStepScope<IFieldSetMapper<FlatFileRecord>, FlatFileRecordMapper>("FlatFileReader/FlatFileReader/FieldSetMapper");

            // Processor - FlatFileReader/Processor
            container.RegisterStepScope<IItemProcessor<FlatFileRecord, FlatFileRecord>,FlatFileRecordProcessor >("FlatFileReader/Processor");

            // Writer - FlatFileReader/DatabaseWriter
            container.StepScopeRegistration<IItemWriter<FlatFileRecord>, DatabaseBatchItemWriter<FlatFileRecord>>("FlatFileReader/DatabaseWriter")
                .Property("ConnectionString").Instance(writerConnectionstring)
                .Property("Query").Value("INSERT INTO BA_FLATFILE_READER_TABLE (CODE,NAME,DESCRIPTION,DATE) VALUES (:code,:name,:description,:date)")
                .Property("DbParameterSourceProvider").Reference<PropertyParameterSourceProvider<FlatFileRecord>>()
                .Register();
            
        }
    }
}