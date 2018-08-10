using System;
using System.Collections.Generic;
using System.Linq;
using Entitas;
using Entitas.VisualDebugging.Unity.Editor;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace SirMetathyst.Entitas.Blueprints.Unity.Editor
{
    [CustomEditor (typeof (BlueprintScriptableObject))]
    public class BlueprintScriptableObjectInspector : UnityEditor.Editor
    {
        [DidReloadScripts, MenuItem ("Tools/Entitas/Blueprints/Update all Blueprints", false, 300)]
        public static void UpdateAllBlueprints ()
        {

            if (!EditorApplication.isPlayingOrWillChangePlaymode)
            {
                var allContexts = GetContextList ();
                if (allContexts == null)
                    return;

                var allContextNames = allContexts.Select (context => context.contextInfo.name).ToArray ();

                var BlueprintList = BlueprintScriptableObjectEditor.GetBlueprintScriptableObjectList ();

                foreach (var Blueprint in BlueprintList)
                {
                    UpdateBlueprintLayout (Blueprint, allContexts, allContextNames);
                }
            }
        }

        static IList<IContext> GetContextList ()
        {
            var contextsType = AppDomain.CurrentDomain
                .GetNonAbstractTypes<IContexts> ()
                .SingleOrDefault ();

            if (contextsType != null)
            {
                var contexts = (IContexts) Activator.CreateInstance (contextsType);
                return contexts.allContexts;
            }

            return null;
        }

        IList<IContext> _allContexts;
        string[] _allContextNames;

        int _contextIndex;
        int _previousContextIndex;

        IContext _context;
        IEntity _entity;

        string previousName;

        void Awake ()
        {
            _allContexts = GetContextList ();
            if (_allContexts == null)
                return;

            var BlueprintScriptableObject = ((BlueprintScriptableObject) target);
            previousName = BlueprintScriptableObject.name;

            _allContextNames = _allContexts.Select (context => context.contextInfo.name).ToArray ();

            UpdateBlueprintLayout ();

            var _blueprint = BlueprintScriptableObject.GetBlueprint ();
            string bpcontext = "";

            if (_blueprint != null)
                bpcontext = _blueprint.context;

            _contextIndex = Array.IndexOf (_allContextNames, bpcontext);
            if (_contextIndex < 0)
            {
                _previousContextIndex = 0;
                _contextIndex = 0;
            }
            else
            {
                _previousContextIndex = _contextIndex;
            }

            SwitchToContext (_contextIndex);

            if (_blueprint == null)
                _blueprint = BlueprintBuilder.Instance.CreateBlueprint (_entity, "New Blueprint");

            _entity.ApplyBlueprint (_blueprint);

            UpdateBlueprint ();
        }

        void OnDisable ()
        {
            if (_context != null)
            {
                _context.Reset ();
            }
        }

        public override void OnInspectorGUI ()
        {
            var BlueprintScriptableObject = ((BlueprintScriptableObject) target);

            EditorGUI.BeginChangeCheck ();
            {
                EditorGUILayout.LabelField ("Blueprint", EditorStyles.boldLabel);

                if (_context != null)
                {
                    EditorGUILayout.BeginHorizontal ();
                    {
                        _contextIndex = EditorGUILayout.Popup (_contextIndex, _allContextNames);
                        if (_previousContextIndex != _contextIndex)
                        {
                            _previousContextIndex = _contextIndex;
                            SwitchToContext (_contextIndex);
                        }
                    }
                    EditorGUILayout.EndHorizontal ();

                    EntityDrawer.DrawComponents (_context, _entity);
                }
                else
                {
                    EditorGUILayout.LabelField ("No contexts found!");
                }
            }
            var changed = EditorGUI.EndChangeCheck ();
            if (changed || previousName != BlueprintScriptableObject.name)
            {
                previousName = BlueprintScriptableObject.name;
                UpdateBlueprintLayout ();
                UpdateBlueprint ();
                EditorUtility.SetDirty (BlueprintScriptableObject);
            }
        }

        public void UpdateBlueprint ()
        {
            UpdateBlueprint (_entity, ((BlueprintScriptableObject) target));
        }

        public void UpdateBlueprint (IEntity entity, BlueprintScriptableObject BlueprintScriptableObject)
        {
            var NewBlueprint = BlueprintBuilder.Instance.CreateBlueprint (entity, BlueprintScriptableObject.name);
            BlueprintScriptableObject.SetBlueprint (NewBlueprint);
        }

        public void UpdateBlueprintLayout ()
        {
            var BlueprintScriptableObject = ((BlueprintScriptableObject) target);
            UpdateBlueprintLayout (BlueprintScriptableObject, _allContexts, _allContextNames);
        }

        public static void UpdateBlueprintLayout (BlueprintScriptableObject Blueprint, IList<IContext> ContextList, IList<string> ContextNameList)
        {
            var blueprint = Blueprint.GetBlueprint ();
            if (blueprint == null)
                return;

            var needsUpdate = false;

            var contextIndex = ContextNameList.IndexOf (blueprint.context);
            if (contextIndex < 0)
            {
                contextIndex = 0;
                needsUpdate = true;
            }

            var context = ContextList[contextIndex];
            blueprint.context = context.contextInfo.name;

            foreach (var component in blueprint.components)
            {
                var type = component.type.ToType ();
                var index = Array.IndexOf (context.contextInfo.componentTypes, type);

                if (index != component.index)
                {
                    Debug.Log (string.Format (
                        "Blueprint '{0}' has invalid or outdated component index for '{1}'. Index was {2} but should be {3}. Updated index.",
                        blueprint.name, component.type, component.index, index));

                    component.index = index;
                    needsUpdate = true;
                }
            }

            if (needsUpdate)
            {
                Debug.Log ("Updating Blueprint '" + blueprint.name + "'");
                Blueprint.SetBlueprint (blueprint);
                EditorUtility.SetDirty (Blueprint);
            }
        }

        void SwitchToContext (int Index)
        {
            if (_context != null)
            {
                _context.Reset ();
            }

            if (Index < _allContexts.Count && Index >= 0)
            {
                var targetContext = _allContexts[Index];
                _context = (IContext) Activator.CreateInstance (targetContext.GetType ());
                _entity = (IEntity) _context.GetType ().GetMethod ("CreateEntity").Invoke (_context, null);
            }
            else
            {
                Debug.Log ("SwitchToContext: Index: " + Index + ":0" + "/" + _allContexts.Count + ":1");
            }
        }
    }
}