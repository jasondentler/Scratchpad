using System;
using MediatR;
using StructureMap;

namespace Scratchpad.StructureMap
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var container = new Container(cfg =>
            {
                cfg.AddRegistry<MediatorRegistry>();
            });

            var mediator = container.GetInstance<Mediator>();
            mediator.Send(new MyCommand());
            mediator.Send(new MyCommand2());
            Console.ReadKey();
        }
    }
}
