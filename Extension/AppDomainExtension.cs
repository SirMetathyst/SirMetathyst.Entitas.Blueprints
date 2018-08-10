using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SirMetathyst.Entitas.Blueprints
{
    public static class AppDomainExtension
    {
        public static IList<Type> GetAllTypes (this AppDomain appDomain)
        {
            return appDomain.GetAssemblies ().GetAllTypes ();
        }

        public static IList<Type> GetAllTypes (this IEnumerable<Assembly> assemblies)
        {
            List<Type> typeList = new List<Type> ();
            foreach (Assembly assembly in assemblies)
            {
                try
                {
                    typeList.AddRange (assembly.GetTypes ());
                }
                catch (ReflectionTypeLoadException ex)
                {
                    typeList.AddRange (ex.Types.Where (type => type != null));
                }
            }
            return typeList.ToList ();
        }

        public static IList<Type> GetNonAbstractTypes<T> (this AppDomain appDomain)
        {
            return appDomain.GetAllTypes ().GetNonAbstractTypes<T> ();
        }

        public static IList<Type> GetNonAbstractTypes<T> (this IList<Type> types)
        {
            return types.Where (type => !type.IsAbstract).Where (type => type.ImplementsInterface<T> ()).ToList ();
        }
    }
}