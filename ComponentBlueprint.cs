using System;
using System.Collections.Generic;

namespace SirMetathyst.Entitas.Blueprints
{
    [Serializable]
    public class ComponentBlueprint
    {
        public int index;
        public string type;
        public SerializableMember[] members;
        public ComponentBlueprint () { }
        public ComponentBlueprint (string type, int index, SerializableMember[] members)
        {
            this.type = type;
            this.index = index;
            this.members = members;
        }
    }
}