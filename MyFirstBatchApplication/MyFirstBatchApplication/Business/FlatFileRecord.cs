using System;

namespace MyFirstBatchApplication.Business
{
    /// <summary>
    /// FlatFileRecord : business object to store the 
    /// read records from the flat file
    /// </summary>
    [Serializable]
    public class FlatFileRecord
    {
        /// <summary>
        /// Property Code.
        /// </summary>
        public int? Code { get; set; }

        /// <summary>
        /// Property Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Property Description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Property Date.
        /// </summary>
        public DateTime? Date { get; set; }

    }
}
