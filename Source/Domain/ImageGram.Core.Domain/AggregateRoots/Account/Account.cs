using ImageGram.Core.Domain.Commons;

namespace ImageGram.Core.Domain.AggregateRoots.Account
{
    public interface Account : IAggregateRoot
    {
        #region Properties

        public string Id { get; set; }
        
        public string Name { get; set; }

        #endregion
    }
}