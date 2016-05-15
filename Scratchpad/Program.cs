using System;
using System.Linq;
using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using MediatR;

namespace Scratchpad
{
    internal class Program
    {
        private static void Main(string[] args)
        {


            var container = new WindsorContainer();

            container.Register(Component.For<IMediator>().ImplementedBy<Mediator>());

            container.Register(
                Component.For<SingleInstanceFactory>()
                    .UsingFactoryMethod(k => new SingleInstanceFactory(k.Resolve)));

            container.Register(
                Component.For<MultiInstanceFactory>()
                    .UsingFactoryMethod(k => new MultiInstanceFactory(t => k.ResolveAll(t).Cast<object>())));

            container.Kernel.AddHandlersFilter(new ContravariantFilter());

            container.Register(
                Classes
                    .FromThisAssembly()
                    .Where(t => t.IsRequestHandler())
                    .WithService.AllInterfaces()
                    .Configure(c =>
                    {
                        var key = c.Implementation.FullName;
                        var tuples = c.Implementation.GetInterfaces()
                            .Where(t => t.IsConstructedGenericType)
                            .Where(t => t.GetGenericTypeDefinition() == typeof(IRequestHandler<,>))
                            .Select(t => t.GetGenericArguments())
                            .Select(p => Tuple.Create(p[0], p[1]));

                        foreach (var tuple in tuples)
                        {
                            container.Register(
                                Component.For(typeof(IRequestHandler<,>).MakeGenericType(tuple.Item1, tuple.Item2))
                                    .ImplementedBy(typeof(Decorator1<,>).MakeGenericType(tuple.Item1, tuple.Item2))
                                    .IsDefault()
                                    .NamedAutomatically(key + "_first") /* my registration key */
                                    .DependsOn(Dependency.OnComponent(
                                        "inner" /*parameter name*/,
                                        key + "_second" /* inner's registration key */)),

                                Component.For(typeof(IRequestHandler<,>).MakeGenericType(tuple.Item1, tuple.Item2))
                                    .ImplementedBy(typeof(Decorator2<,>).MakeGenericType(tuple.Item1, tuple.Item2))
                                    .NamedAutomatically(key + "_second")
                                    .IsFallback()
                                    .DependsOn(Dependency.OnComponent("inner", key + "_handler"))
                                );
                        }

                        c.NamedAutomatically(key + "_handler")
                            .IsFallback();
                    })
                );

            container.LogRegisteredComponents();

            var mediator = container.Resolve<IMediator>();

            mediator.Send(new MyCommand());
            mediator.Send(new MyCommand2());
            Console.ReadKey();
        }

    }
}

