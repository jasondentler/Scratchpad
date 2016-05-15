using System;
using MediatR;

namespace Scratchpad.StructureMap
{
    public class MyCommandHandler : RequestHandler<MyCommand>
    {
        protected override void HandleCore(MyCommand message)
        {
            Console.WriteLine("Handling {0} #{1}", message.GetType().Name, message.RequestId);
        }
    }
}