using FluentValidation.Results;
using System;

namespace IMDb.Core
{
    public abstract class Entity
    {
        protected Entity()
        {

        }

        public long Id { get; set; }
        public Guid Guid { get; set; }
        public ValidationResult ValidationResult { get; protected set; }
    }
}
