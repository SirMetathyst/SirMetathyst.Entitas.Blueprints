using System;
using System.Collections.Generic;

namespace SirMetathyst.Entitas.Blueprints
{
    [Serializable]
    public class ComponentBlueprint
    {
        public int index;
        public object value;

        public ComponentBlueprint () { }
        public ComponentBlueprint (int index, object value)
        {
            this.index = index;
            this.value = value;
        }
    }
}