using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace SirMetathyst.Entitas.Blueprints
{
    public static partial class BlueprintManagerExtension
    {
        public static BlueprintManager Provide<T> (this BlueprintManager manager) where T : class, IBlueprintProvider
        {
            var provider = Activator.CreateInstance<T> ();
            var blueprintList = provider.GetBlueprintList ();
            manager.AddBlueprintRange (blueprintList);
            return manager;
        }

        public static BlueprintManager Provide<T> (this BlueprintManager manager, T provider) where T : class, IBlueprintProvider
        {
            var blueprintList = provider.GetBlueprintList ();
            manager.AddBlueprintRange (blueprintList);
            return manager;
        }
    }
}