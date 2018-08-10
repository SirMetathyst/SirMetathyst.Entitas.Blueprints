using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SirMetathyst.Entitas.Blueprints.Unity
{
    public class BlueprintProviderBehaviour : MonoBehaviour, IBlueprintProvider
    {
        [SerializeField]
        List<BlueprintScriptableObject> BlueprintList;

        public IList<Blueprint> GetBlueprintList ()
        {
            return BlueprintList.Select (x => x.GetBlueprint ()).ToList ();
        }
    }
}