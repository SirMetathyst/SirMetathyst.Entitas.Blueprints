using UnityEditor;
using UnityEngine;

namespace SirMetathyst.Entitas.Blueprints.Unity.Editor
{
    [CustomEditor (typeof (BlueprintListScriptableObject))]
    public class BlueprintListScriptableObjectInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI ()
        {
            var blueprintListScriptableObject = ((BlueprintListScriptableObject) target);

            EditorGUILayout.BeginVertical ();
            {
                EditorGUI.BeginDisabledGroup (true);
                {
                    foreach (var blueprint in blueprintListScriptableObject.GetBlueprintList ())
                    {
                        EditorGUILayout.ObjectField (blueprint, typeof (BlueprintScriptableObject), false);
                    }
                }
                EditorGUI.EndDisabledGroup ();
            }
            EditorGUILayout.EndVertical ();

            if (GUILayout.Button ("Find all Blueprints"))
            {
                var blueprintList = BlueprintScriptableObjectEditor.GetBlueprintScriptableObjectList ();
                blueprintListScriptableObject.SetBlueprintList (blueprintList);
                EditorUtility.SetDirty (blueprintListScriptableObject);
            }
        }
    }
}