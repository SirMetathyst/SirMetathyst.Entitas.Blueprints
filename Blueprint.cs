using System;
using Entitas;

namespace SirMetathyst.Entitas.Blueprints
{
    [Serializable]
    public class Blueprint
    {
        public string context;
        public string name;
        public ComponentBlueprint[] components;

        public Blueprint (string context, string name, ComponentBlueprint[] components)
        {
            this.context = context;
            this.name = name;
            this.components = components;
        }
    }
}