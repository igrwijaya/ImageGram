using System;

namespace ImageGram.Core.Domain.Commons
{
    public abstract class CoreEntity
    {
        #region Entity Properties

        public int Id { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? LastModifiedDateTime { get; set; }

        public string LastModifiedBy { get; set; }

        #endregion

        #region Protected Methods

        protected abstract void EnsureValidState();

        protected void Apply(Action action)
        {
            action();
            EnsureValidState();
        }

        #endregion
    }
}