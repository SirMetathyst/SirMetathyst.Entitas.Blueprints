using System;
using System.Collections.Generic;
using System.Reflection;

namespace SirMetathyst.Entitas.Blueprints
{
    public static class PublicMemberInfoExtension
    {
        public static IList<PublicMemberInfo> GetPublicMemberInfoList (this Type type)
        {
            FieldInfo[] fields = type.GetFields (BindingFlags.Instance | BindingFlags.Public);
            PropertyInfo[] properties = type.GetProperties (BindingFlags.Instance | BindingFlags.Public);
            List<PublicMemberInfo> publicMemberInfoList = new List<PublicMemberInfo> (fields.Length + properties.Length);

            for (int index = 0; index < fields.Length; ++index)
            {
                publicMemberInfoList.Add (new PublicMemberInfo (fields[index]));
            }

            for (int index = 0; index < properties.Length; ++index)
            {
                PropertyInfo info = properties[index];
                if (info.CanRead && info.CanWrite && info.GetIndexParameters ().Length == 0)
                {
                    publicMemberInfoList.Add (new PublicMemberInfo (info));
                }
            }

            return publicMemberInfoList;
        }
    }
}