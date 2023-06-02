using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GameCore.Utils
{
    public static class FinderUtils
    {
        public static class TypeFinder
        {
            private static IEnumerable<Assembly> ProjectAssemblies => AppDomain.CurrentDomain.GetAssemblies();
            private static IEnumerable<Type> ProjectTypes => ProjectAssemblies.SelectMany(assembly => assembly.GetTypes());

            public static IEnumerable<Type> FindChildrenTypes(Type parentType, bool canBeAbstract = false)
            {
                var childrenTypes =
                    ProjectTypes.Where(type => type != parentType && IsSubclassOfOrImplements(parentType, type));
                return canBeAbstract
                    ? childrenTypes
                    : childrenTypes.Where(type => !type.IsAbstract && !type.IsInterface);
            }

            public static IEnumerable<Type> FindAllGenericParametersForType(Type someType)
            {
                return !someType.IsGenericType ? FindAllGenericParametersForType(someType.BaseType) : someType.GetTypeInfo().GenericTypeArguments;
            }

            private static bool IsSubclassOfOrImplements(Type parentType, Type type)
            {
                return parentType.IsAssignableFrom(type) ||
                       type.GetInterfaces().Any(interfaceType =>
                           IsGenericTypeDefinitionMatch(parentType, interfaceType));
            }

            private static bool IsGenericTypeDefinitionMatch(Type parentType, Type interfaceType)
            {
                return parentType.IsGenericTypeDefinition &&
                       interfaceType.IsGenericType &&
                       parentType == interfaceType.GetGenericTypeDefinition();
            }
        }
    }
}
