using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace ImageGram.Core.Application.Commons
{
    public class BaseQueryResult<TData> : BaseQueryResult
    {
        #region Constructors

        public BaseQueryResult()
        {
            
        }
        
        public BaseQueryResult(List<ValidationFailure> failures)
        {
            foreach (var validationFailure in failures)
            {
                _errorMessages.Add(validationFailure.ErrorMessage);
            }
        }

        #endregion
        
        #region Properties

        public TData Data { get; set; }

        #endregion
    }

    public class BaseQueryResult
    {
        #region Fields

        protected readonly List<string> _errorMessages = new();
        
        private readonly List<string> _successMessages = new();

        #endregion
        
        #region Constructors

        public BaseQueryResult()
        {
            
        }
        
        public BaseQueryResult(List<ValidationFailure> failures)
        {
            foreach (var validationFailure in failures)
            {
                _errorMessages.Add(validationFailure.ErrorMessage);
            }
        }

        #endregion
        
        #region Properties

        public bool IsSuccess => !_errorMessages.Any();

        public List<string> ErrorMessages => _errorMessages ?? new List<string>();

        public List<string> SuccessMessages => _successMessages ?? new List<string>();

        #endregion

        #region Public Methods

        public void AddErrorMessage(string message)
        {
            _errorMessages.Add(message);
        }

        public void AddErrorMessages(IEnumerable<string> messages)
        {
            _errorMessages.AddRange(messages);
        }

        public void AddSuccessMessage(string message)
        {
            _successMessages.Add(message);
        }

        public void AddSuccessMessages(IEnumerable<string> messages)
        {
            _successMessages.AddRange(messages);
        }

        #endregion
    }
}