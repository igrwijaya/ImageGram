using MediatR;

namespace ImageGram.Core.Application.Commons
{
    public class BaseDataTableQuery<TResult> : IRequest<BaseDataTableResult<TResult>>
    {
        #region Constructors

        public BaseDataTableQuery(string draw, int start, int length, string sortColumn, string sortColumnDirection, string searchParam)
        {
            Draw = draw;
            Start = start;
            Length = length;
            SortColumn = sortColumn;
            SortColumnDirection = sortColumnDirection;
            SearchParam = searchParam;
        }

        #endregion
        
        #region Properties

        public string Draw { get; private set; }

        public int Start { get; private set; }

        public int Length { get; private set; }

        public string SortColumn { get; private set; }

        public string SortColumnDirection { get; private set; }

        public string SearchParam { get; private set; }

        #endregion
    }
}