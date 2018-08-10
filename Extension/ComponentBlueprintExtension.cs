using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace SirMetathyst.Entitas.Blueprints
{
    public static partial class ComponentBlueprintExtension
    {
        public static IComponent CreateComponent (this ComponentBlueprint blueprint, IEntity entity)
        {
            Type _type;
            Dictionary<string, PublicMemberInfo> _componentMembers;

            _type = blueprint.type.ToType ();

            if (_type == null)
            {
                throw new ComponentBlueprintException (
                    "Type '" + blueprint.type +
                    "' doesn't exist in any assembly!",
                    "Please check the full type name."
                );
            }

            if (!_type.ImplementsInterface<IComponent> ())
            {
                throw new ComponentBlueprintException (
                    "Type '" + blueprint.type +
                    "' doesn't implement IComponent!",
                    typeof (ComponentBlueprint).Name +
                    " only supports IComponent."
                );
            }

            var component = entity.CreateComponent (blueprint.index, _type);

            var memberInfos = component.GetType ().GetPublicMemberInfoList ();
            _componentMembers = new Dictionary<string, PublicMemberInfo> (memberInfos.Count);
            for (int i = 0; i < memberInfos.Count; i++)
            {
                var info = memberInfos[i];
                _componentMembers.Add (info.name, info);
            }

            for (int i = 0; i < blueprint.members.Length; i++)
            {
                var member = blueprint.members[i];

                PublicMemberInfo memberInfo;
                if (_componentMembers.TryGetValue (member.name, out memberInfo))
                {
                    object obj = null;

                    if (!memberInfo.type.IsEnum)
                    {
                        obj = Convert.ChangeType (member.value, memberInfo.type);
                    }
                    else
                    {
                        obj = Enum.Parse (memberInfo.type, (string) member.value);
                    }

                    memberInfo.SetValue (component, obj);
                }
                else
                {
                    throw new Exception (
                        "Could not find member '" + member.name +
                        "' in type '" + _type.FullName + "'!\n" +
                        "Only non-static public members are supported."
                    );
                }
            }

            return component;
        }
    }
}