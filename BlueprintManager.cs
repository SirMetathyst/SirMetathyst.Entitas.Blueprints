using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace SirMetathyst.Entitas.Blueprints
{
    public partial class BlueprintManager
    {
        Dictionary<string, Blueprint> Container = new Dictionary<string, Blueprint> ();

        static BlueprintManager _Instance;

        public static BlueprintManager Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new BlueprintManager ();

                return _Instance;
            }
        }

        public BlueprintManager () { }

        public BlueprintManager AddBlueprint (Blueprint value)
        {
            Container.Add (value.name, value);
            return this;
        }

        public BlueprintManager AddBlueprintRange (IEnumerable<Blueprint> collection)
        {
            foreach (var blueprint in collection)
            {
                AddBlueprint (blueprint);
            }

            return this;
        }

        public bool TryGetBlueprint (string name, out Blueprint value)
        {
            return Container.TryGetValue (name, out value);
        }

        public Blueprint GetBlueprint (string name)
        {
            Blueprint value = null;
            TryGetBlueprint (name, out value);
            return value;
        }

        public IList<Blueprint> GetBlueprintList ()
        {
            return Container.Values.ToList ();
        }
    }
}