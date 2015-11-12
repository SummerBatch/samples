using Summer.Batch.Extra;
using Summer.Batch.Infrastructure.Item.File.Mapping;
using Summer.Batch.Infrastructure.Item.File.Transform;

namespace MyFirstBatchApplication.Business.Mappers
{
    public class FlatFileRecordMapper : IFieldSetMapper<FlatFileRecord>
    {
        private IDateParser _dateParser = new DateParser();

        /// <summary>
        /// Parser for date columns.
        /// </summary>
        private IDateParser DateParser { set { _dateParser = value; } }

        /// <summary>
        /// Maps a <see cref="IFieldSet"/> to a <see cref="FlatFileRecord" />.
        /// <param name="fieldSet">the field set to map</param>
        /// <returns>the corresponding item</returns>
        /// </summary>
        public FlatFileRecord MapFieldSet(IFieldSet fieldSet)
        {
            // Create a new instance of the current mapped object
            return new FlatFileRecord
            {
                Code = fieldSet.ReadInt(0),
                Name = fieldSet.ReadString(1), // ReadString trims the read string, use ReadRawString to keep trailing spaces
                Description = fieldSet.ReadString(2),
                Date = _dateParser.Decode(fieldSet.ReadString(3))
            };
        }
    }
    
}