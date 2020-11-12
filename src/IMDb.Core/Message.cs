using System;

namespace IMDb.Core
{
    public abstract class Message
    {
        protected Message() { }

        public string MessageType { get; protected set; }
        public Guid AggregateId { get; protected set; }
    }
}
