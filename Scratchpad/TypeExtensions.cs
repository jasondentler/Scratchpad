using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Castle.DynamicProxy.Internal;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Naming;
using Castle.Windsor;
using MediatR;

namespace Scratchpad
{
    public static class TypeExtensions
    {
        public static bool IsRequestHandler(this Type type)
        {
            return type != null &&
                   !type.IsAbstract &&
                   !type.IsGenericTypeDefinition &&
                   type.GetAllInterfaces().Any(i => i.IsConstructedGenericType &&
                                                    i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>));
        }

        /// <summary>
        /// Log the registered components in the container.
        /// </summary>
        /// <param name="container">The container that has the component registry to be logged.</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void LogRegisteredComponents(this IWindsorContainer container)
        {
            var sb = new StringBuilder("COMPONENTS IN WINDSOR CONTAINER:\n");
            var naming = container.Kernel.GetSubSystem(SubSystemConstants.NamingKey) as
                INamingSubSystem;
            const string formatString = "{0,-40}{1, -40}{2}\n";
            sb.AppendLine();
            sb.AppendFormat(formatString, "Key", "Interface", "Class");

            var components = new List<string>();
            if (naming != null)
                components.AddRange(from handler in naming.GetAllHandlers()
                    from service in handler.ComponentModel.Services
                    select string.Format(formatString, handler.ComponentModel.Name, service.Name, handler.ComponentModel.Implementation.FullName));
            components.Sort();
            components.ForEach(c => sb.Append(c));
            sb.Append("****************************************************");
            Console.WriteLine(sb.ToString());
        }

    }
}