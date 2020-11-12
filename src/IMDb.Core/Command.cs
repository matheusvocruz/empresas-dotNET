using FluentValidation.Results;
using MediatR;
using System;

namespace IMDb.Core
{
    public abstract class Command : Message, IRequest<ValidationResult>
    {
        protected Command() 
        {
            Timestamp = DateTime.Now;
        }

        public DateTime Timestamp { get; }
        public ValidationResult ValidationResult { get; set; }

        public virtual bool IsValid() 
        {
            throw new NotImplementedException();
        }
    }
}
