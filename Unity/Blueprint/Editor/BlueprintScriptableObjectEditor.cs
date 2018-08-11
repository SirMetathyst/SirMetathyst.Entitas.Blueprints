using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace SirMetathyst.Entitas.Blueprints.Unity.Editor
{
    public static class BlueprintScriptableObjectEditor
    {
        public static List<BlueprintScriptableObject> GetBlueprintScriptableObjectList ()
        {
            return AssetDatabase.FindAssets ("l:" + BlueprintScriptableObjectPostprocessor.ASSET_LABEL)
                .Select (AssetDatabase.GUIDToAssetPath)
                .Select (AssetDatabase.LoadAssetAtPath<BlueprintScriptableObject>)
                .ToList ();
        }
    }
}