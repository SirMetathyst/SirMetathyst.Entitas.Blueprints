using System.Collections.Generic;
using Entitas;

namespace SirMetathyst.Entitas.Blueprints
{
    public class BlueprintBuilder
    {
        static BlueprintBuilder _Instance;

        public static BlueprintBuilder Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new BlueprintBuilder ();

                return _Instance;
            }
        }

        public Blueprint CreateBlueprint (IEntity entity, string name)
        {
            var allComponents = entity.GetComponents ();
            var componentIndices = entity.GetComponentIndices ();
            var components = new List<ComponentBlueprint> ();

            for (int i = 0; i < allComponents.Length; i++)
            {
                var t = allComponents[i].GetType ();
                if (t.IsDefined (typeof (System.SerializableAttribute), false))
                {
                    components.Add (new ComponentBlueprint (componentIndices[i], allComponents[i]));
                }
            }

            return new Blueprint (entity.contextInfo.name, name, components.ToArray ());
        }
    }
}