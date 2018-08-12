using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace SirMetathyst.Entitas.Blueprints.Unity
{
    public static partial class BlueprintManagerExtension
    {
        public static BlueprintManager RegisterUnityBlueprintList (this BlueprintManager manager)
        {
            var blueprintProviderListBehaviour = GameObject.FindObjectOfType<BlueprintListProviderBehaviour> ();
            var blueprintProviderBehaviourList = GameObject.FindObjectsOfType<BlueprintProviderBehaviour> ();

            manager.Provide<BlueprintListProviderBehaviour> (blueprintProviderListBehaviour);

            foreach (var blueprintProviderBehaviour in blueprintProviderBehaviourList)
            {
                manager.Provide<BlueprintProviderBehaviour> (blueprintProviderBehaviour);
            }

            return manager;
        }
    }
}