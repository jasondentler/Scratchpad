using System;
using MediatR;

namespace Scratchpad.StructureMap
{
    public abstract class Command : Command<Unit>, IRequest
    {
    }

    public abstract class Command<T> : IRequest<T>
    {
        public Guid RequestId { get; private set; }

        protected Command() : this(Guid.NewGuid())
        {
        }

        protected Command(Guid requestId)
        {
            RequestId = requestId;
        }
    }
}