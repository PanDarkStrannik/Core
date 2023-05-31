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

            public static IEnumerable<Type> FindChildrenTypes(Type baseType)
            {
                return ProjectTypes.Where(baseType.IsInstanceOfType);
            }

            public static IEnumerable<Type> FindAllGenericParametersForType(Type someType)
            {
                var typeArguments = someType.GetGenericArguments();
                return ProjectTypes.Where(projectType =>
                    typeArguments.Any(projectType.IsInstanceOfType));
            }
        }
    }
}
