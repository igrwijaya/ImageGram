namespace ImageGram.Core.Domain.Commons
{
    public class CoreDataTableParameter
    {
        #region Properties

        public string Draw { get; set; }

        public int Start { get; set; }

        public int Length { get; set; }

        public string SortColumn { get; set; }

        public string SortColumnDirection { get; set; }

        public string SearchParam { get; set; }

        #endregion
    }
}