using UnityEngine;

namespace SirMetathyst.Entitas.Blueprints.Unity
{
    [CreateAssetMenu (menuName = "Entitas/Blueprint", fileName = "New Blueprint.asset")]
    public class BlueprintScriptableObject : ScriptableObject
    {
        [SerializeField]
        Blueprint _blueprint;

        public void SetBlueprint (Blueprint blueprint)
        {
            _blueprint = blueprint;
        }

        public Blueprint GetBlueprint ()
        {
            return _blueprint;
        }
    }
}