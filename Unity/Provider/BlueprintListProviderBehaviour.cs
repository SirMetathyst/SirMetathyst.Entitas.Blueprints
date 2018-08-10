using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SirMetathyst.Entitas.Blueprints.Unity
{
    public class BlueprintListProviderBehaviour : MonoBehaviour, IBlueprintProvider
    {
        [SerializeField]
        BlueprintListScriptableObject BlueprintList;

        public IList<Blueprint> GetBlueprintList ()
        {
            return BlueprintList.GetBlueprintList ().Select (x => x.GetBlueprint ()).ToList ();
        }
    }
}