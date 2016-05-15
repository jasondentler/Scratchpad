using System;
using MediatR;

namespace Scratchpad
{
    public class MyCommandHandler2 : RequestHandler<MyCommand2>
    {
        protected override void HandleCore(MyCommand2 message)
        {
            Console.WriteLine("Executing MyCommandHandler2 for {0}", message.Id);
        }
    }
}