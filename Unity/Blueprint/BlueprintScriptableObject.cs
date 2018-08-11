using UnityEngine;

namespace SirMetathyst.Entitas.Blueprints.Unity
{
    public abstract class BlueprintScriptableObject : ScriptableObject
    {
        public abstract void SetBlueprint (Blueprint blueprint);
        public abstract Blueprint GetBlueprint ();
    }
}