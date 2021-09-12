using System.Collections.Generic;

namespace ImageGram.Core.Application.Commons
{
    public class BaseDataTableResult<TDto>
    {
        #region Properties

        public string Draw { get; set; }

        public int RecordsFiltered { get; set; }

        public int RecordsTotal { get; set; }

        public List<TDto> Data { get; set; }

        #endregion
    }
}