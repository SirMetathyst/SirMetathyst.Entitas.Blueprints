using System;
using Entitas;

namespace SirMetathyst.Entitas.Blueprints.Unity
{
    [Serializable]
    public class JsonBlueprint
    {
        public string context;
        public string name;
        public JsonComponentBlueprint[] components;

        public JsonBlueprint (string context, string name, JsonComponentBlueprint[] components)
        {
            this.context = context;
            this.name = name;
            this.components = components;
        }
    }
}