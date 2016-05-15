using System;
using MediatR;

namespace Scratchpad.StructureMap
{
    public class Decorator2<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> 
        where TRequest : IRequest<TResponse>
    {
        private readonly IRequestHandler<TRequest, TResponse> _inner;

        public Decorator2(IRequestHandler<TRequest, TResponse> inner)
        {
            _inner = inner;
        }

        public TResponse Handle(TRequest message)
        {
            try
            {
                Console.WriteLine("Entering Decorator2");
                return _inner.Handle(message);
            }
            finally
            {
                Console.WriteLine("Exiting Decorator2");
            }
        }
    }
}