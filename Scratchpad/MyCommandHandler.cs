using System;
using MediatR;

namespace Scratchpad
{
    public class MyCommandHandler : RequestHandler<MyCommand>
    {
        protected override void HandleCore(MyCommand message)
        {
            Console.WriteLine("Executing MyCommandHandler for {0}", message.Id);
        }
    }
}