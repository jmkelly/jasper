using System;
using System.Linq;
using System.Reflection;
using Jasper.Internals.Util;
using Microsoft.Extensions.DependencyInjection;

namespace Jasper.Internals.Scanning.Conventions
{
    public class DefaultConventionScanner : IRegistrationConvention
    {
        public void ScanTypes(TypeSet types, IServiceCollection services)
        {
            foreach (var type in types.FindTypes(TypeClassification.Concretes).Where(type => type.HasConstructors()))
            {
                var pluginType = FindPluginType(type);
                if (pluginType != null)
                {
                    services.AddType(pluginType, type);
                }
            }
        }

        public virtual Type FindPluginType(Type concreteType)
        {
            var interfaceName = "I" + concreteType.Name;
            return concreteType.GetTypeInfo().GetInterfaces().FirstOrDefault(t => t.Name == interfaceName);
        }

        public override string ToString()
        {
            return "Default I[Name]/[Name] registration convention";
        }
    }
}
