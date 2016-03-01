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
using Summer.Batch.Extra.Sort;
using Summer.Batch.Core.Step.Tasklet;
using System;
using System.Collections.Generic;
using System.IO;

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
            RegisterStepFlatFileReaderTasklet(container);
        }

        private static void RegisterStepFlatFileReaderTasklet(IUnityContainer container)
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
            container.RegisterStepScope<IItemProcessor<FlatFileRecord, FlatFileRecord>, FlatFileRecordProcessor>("FlatFileReader/Processor");

            // Writer - FlatFileReader/DatabaseWriter
            container.StepScopeRegistration<IItemWriter<FlatFileRecord>, DatabaseBatchItemWriter<FlatFileRecord>>("FlatFileReader/DatabaseWriter")
                .Property("ConnectionString").Instance(writerConnectionstring)
                .Property("Query").Value("INSERT INTO BA_FLATFILE_READER_TABLE (CODE,NAME,DESCRIPTION,DATE) VALUES (:code,:name,:description,:date)")
                .Property("DbParameterSourceProvider").Reference<PropertyParameterSourceProvider<FlatFileRecord>>()
                .Register();

        }

        private void RegisterStep0Tasklet(IUnityContainer container)
        {
            //IList<OutputFile> list = new List<OutputFile>();
            //var outputFile1 = new OutputFile
            //{
            //    Include = "75,2,CH,EQ,C'RD'",
            //    Outrec = "25,200"
            //};
            //var outputFile2 = new OutputFile
            //{
            //    Include = "75,2,CH,EQ,C'XD'"
            //};
            //var outputFile3 = new OutputFile
            //{
            //    Include = "75,2,CH,EQ,C'CD'"
            //};
            //var outputFile4 = new OutputFile
            //{
            //    Include = "75,2,CH,EQ,C'YD'"
            //};
            //var outputFile5 = new OutputFile
            //{

            //};
            //list.Add(outputFile1);
            //list.Add(outputFile2);
            //list.Add(outputFile3);
            //list.Add(outputFile4);
            //list.Add(outputFile5);
            //container.stepscoperegistration<itasklet, SortTasklet>("step0batchlet")
            //.property("input").resources("#{settings['sortjob.step0.inputfiles']}")
            //.property("output").resource("#{settings['sortjob.step0.outputfile']}")
            //.property("headersize").value(0)
            ////.property("outputfile").value(list)
            //.property("include").value("(75,2,bi,ne,x'5244')")
            //.property("separator").value(environment.newline)
            //.property("sortcard").value("format=ch,fields=(121,14,ch,a)")
            //.register();

            container.StepScopeRegistration<ITasklet, SortTasklet>("step0Batchlet")
               .Property("Input").Resources("#{settings['SORTJOB.step0.inputFiles']}")
               .Property("Output").Resource("#{settings['SORTJOB.step0.outputFile']}")
               .Property("HeaderSize").Value(0)
               .Property("Include").Value("(75,2,CH,NE,C'RD')")
               .Property("Separator").Value(Environment.NewLine)
               .Property("SortCard").Value("FORMAT=CH,FIELDS=(121,14,CH,A)")
               .Register();
        }

        // Step step1 - Sort step
        private void RegisterStep1Tasklet(IUnityContainer container)
        {
            container.StepScopeRegistration<ITasklet, SortTasklet>("step1Batchlet")
                .Property("Input").Resources("#{settings['SORTJOB.step1.inputFiles']}")
                .Property("Output").Resource("#{settings['SORTJOB.step1.outputFile']}")
                .Property("HeaderSize").Value(0)
                .Property("Include").Value("(75,2,CH,NE,C'RD')")
                .Property("Separator").Value(Environment.NewLine)
                .Property("SortCard").Value("FORMAT=CH,FIELDS=(121,14,CH,A)")
                .Register();
        }
    }
}