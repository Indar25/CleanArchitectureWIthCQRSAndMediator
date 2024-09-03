using AutoMapper;
using System.Reflection;

namespace CleanArchitectureWIthCQRSAndMediator.Application.Blogs.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappingFromAssembly(Assembly.GetExecutingAssembly());
        }

        private void ApplyMappingFromAssembly(Assembly assembly)
        {
            var mapFromType = typeof(IMapFrom<>);
            var mappingMethodName = nameof(IMapFrom<object>.Mapping);

            bool hasInterface(Type type) => type.IsGenericType && type.GetGenericTypeDefinition() == mapFromType;

            var types = assembly.GetExportedTypes()
                                .Where(t => t.GetInterfaces().Any(hasInterface))
                                .ToList();

            var argumentTypes = new Type[] { typeof(Profile) };

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);
                var methodInfo = type.GetMethod(mappingMethodName);

                if (methodInfo != null)
                {
                    // Invoke the Mapping method directly if it exists on the type itself
                    methodInfo.Invoke(instance, new object[] { this });
                }
                else
                {
                    // If the method is not on the type itself, check the interfaces
                    var interfaces = type.GetInterfaces().Where(hasInterface).ToList();

                    foreach (var @interface in interfaces)
                    {
                        var interfaceMethodInfo = @interface.GetMethod(mappingMethodName, argumentTypes);

                        if (interfaceMethodInfo != null)
                        {
                            interfaceMethodInfo.Invoke(instance, new object[] { this });
                        }
                    }
                }
            }
        }
    }
}
