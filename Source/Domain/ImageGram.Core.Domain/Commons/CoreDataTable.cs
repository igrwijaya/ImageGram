using System.Collections.Generic;

namespace ImageGram.Core.Domain.Commons
{
    public class CoreDataTable<TDataTable>
    {
        #region Properties

        public string Draw { get; set; }

        public int RecordsFiltered { get; set; }

        public int RecordsTotal { get; set; }

        public List<TDataTable> Data { get; set; }

        #endregion
    }
}