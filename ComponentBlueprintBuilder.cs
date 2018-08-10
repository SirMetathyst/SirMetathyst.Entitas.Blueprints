using Entitas;

namespace SirMetathyst.Entitas.Blueprints
{
    public class ComponentBlueprintBuilder : IComponentBlueprintBuilder
    {
        static ComponentBlueprintBuilder _Instance;

        public static ComponentBlueprintBuilder Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new ComponentBlueprintBuilder ();

                return _Instance;
            }
        }

        public ComponentBlueprint CreateComponentBlueprint (int index, IComponent component)
        {
            var type = component.GetType ();
            var typeName = type.FullName;

            var memberInfos = type.GetPublicMemberInfoList ();
            var members = new SerializableMember[memberInfos.Count];

            for (int i = 0; i < memberInfos.Count; i++)
            {
                var info = memberInfos[i];
                members[i] = new SerializableMember (info.name, info.GetValue (component));
            }

            return new ComponentBlueprint (typeName, index, members);
        }
    }
}