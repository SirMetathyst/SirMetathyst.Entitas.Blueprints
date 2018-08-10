using System.Collections.Generic;
using UnityEngine;

namespace SirMetathyst.Entitas.Blueprints.Unity
{
    [CreateAssetMenu (menuName = "Entitas/Blueprint List", fileName = "BlueprintList.asset")]
    public class BlueprintListScriptableObject : ScriptableObject
    {
        [SerializeField]
        List<BlueprintScriptableObject> _blueprintScriptableObjectList;

        public void SetBlueprintList (List<BlueprintScriptableObject> collection)
        {
            this._blueprintScriptableObjectList = collection;
        }

        public List<BlueprintScriptableObject> GetBlueprintList ()
        {
            return _blueprintScriptableObjectList;
        }
    }
}