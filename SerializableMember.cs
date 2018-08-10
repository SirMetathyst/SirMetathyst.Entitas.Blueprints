using System;
using UnityEngine;

namespace SirMetathyst.Entitas.Blueprints
{
    [Serializable]
    public class SerializableMember
    {
        public string name;
        public string type;
        public string value;

        public SerializableMember () { }
        public SerializableMember (string name, object value)
        {
            this.name = name;
            this.type = value.GetType ().FullName;
            this.value = Convert.ToString (value);
        }
    }
}