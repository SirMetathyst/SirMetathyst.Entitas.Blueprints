using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEngine;

namespace SirMetathyst.Entitas.Blueprints
{
    public static class SerializationTypeExtension
    {
        public static Type ToType (this string typeString)
        {
            var type = Type.GetType (typeString);
            if (type != null)
                return type;

            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies ())
            {
                type = assembly.GetType (typeString);
                if (type != null)
                    return type;
            }

            return (Type) null;
        }
    }
}