using System;
using MediatR;

namespace Scratchpad
{
    public class MyCommand : IRequest
    {
        public Guid Id { get; private set; }
        public MyCommand()
        {
            Id = Guid.NewGuid();
        }
    }
}