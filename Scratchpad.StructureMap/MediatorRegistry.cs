using MediatR;
using StructureMap;

namespace Scratchpad.StructureMap
{
    public class MediatorRegistry : Registry
    {
        public MediatorRegistry()
        {
            Scan(scanner =>
            {
                scanner.AssemblyContainingType<MyCommandHandler>();
                scanner.WithDefaultConventions();

                scanner.ConnectImplementationsToTypesClosing(typeof(IRequestHandler<,>));
                scanner.ConnectImplementationsToTypesClosing(typeof(IAsyncRequestHandler<,>));
                scanner.ConnectImplementationsToTypesClosing(typeof(INotificationHandler<>));
                scanner.ConnectImplementationsToTypesClosing(typeof(IAsyncNotificationHandler<>));
            });

            For<SingleInstanceFactory>()
                .Use("Single Instance Factory", ctx => new SingleInstanceFactory(ctx.GetInstance));

            For<MultiInstanceFactory>()
                .Use("Multi Instance Factory", ctx => new MultiInstanceFactory(ctx.GetAllInstances));

            For(typeof(IRequestHandler<,>)).DecorateAllWith(typeof(Decorator1<,>));
            For(typeof(IRequestHandler<,>)).DecorateAllWith(typeof(Decorator2<,>));
        }
    }
}