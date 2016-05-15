using System;
using MediatR;

namespace Scratchpad.StructureMap
{
    public class Decorator1<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> 
        where TRequest : IRequest<TResponse>
    {
        private readonly IRequestHandler<TRequest, TResponse> _inner;

        public Decorator1(IRequestHandler<TRequest, TResponse> inner)
        {
            _inner = inner;
        }

        public TResponse Handle(TRequest message)
        {
            try
            {
                Console.WriteLine("Entering Decorator1");
                return _inner.Handle(message);
            }
            finally
            {
                Console.WriteLine("Exiting Decorator1");
            }
        }
    }
}