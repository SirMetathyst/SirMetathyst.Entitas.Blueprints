using System;
using System.Collections.Generic;

namespace SirMetathyst.Entitas.Blueprints.Unity
{
    [Serializable]
    public class JsonComponentBlueprint
    {
        public int index;
        public Type type;
        public object value;

        public JsonComponentBlueprint () { }
        public JsonComponentBlueprint (int index, object value)
        {
            this.index = index;
            this.type = value.GetType ();
            this.value = value;
        }
    }
}