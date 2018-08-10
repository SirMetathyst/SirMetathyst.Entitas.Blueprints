using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SirMetathyst.Entitas.Blueprints.Unity
{
    public class RegisterBlueprintBehaviour : MonoBehaviour
    {
        void Awake ()
        {
            var blueprintProviderListBehaviour = GameObject.FindObjectOfType<BlueprintListProviderBehaviour> ();
            var blueprintProviderBehaviourList = GameObject.FindObjectsOfType<BlueprintProviderBehaviour> ();

            BlueprintManager.Instance.Provide<BlueprintListProviderBehaviour> (blueprintProviderListBehaviour);

            foreach (var blueprintProviderBehaviour in blueprintProviderBehaviourList)
            {
                BlueprintManager.Instance.Provide<BlueprintProviderBehaviour> (blueprintProviderBehaviour);
            }
        }
    }
}