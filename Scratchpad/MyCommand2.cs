using System;
using MediatR;

namespace Scratchpad
{
    public class MyCommand2 : IRequest
    {
        public Guid Id { get; private set; }

        public MyCommand2()
        {
            Id = Guid.NewGuid();;
        }
    }
}