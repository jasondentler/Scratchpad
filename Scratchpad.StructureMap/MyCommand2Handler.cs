using System;
using MediatR;

namespace Scratchpad.StructureMap
{
    public class MyCommand2Handler : RequestHandler<MyCommand2>
    {
        protected override void HandleCore(MyCommand2 message)
        {
            Console.WriteLine("Handling {0} #{1}", message.GetType().Name, message.RequestId);
        }
    }
}