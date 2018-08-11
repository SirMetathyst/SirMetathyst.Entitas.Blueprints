using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using UnityEngine;

namespace SirMetathyst.Entitas.Blueprints.Unity
{
    [CreateAssetMenu (menuName = "Entitas/Json Blueprint", fileName = "New Json Blueprint.asset")]
    public class JsonBlueprintScriptableObject : BlueprintScriptableObject
    {
        [SerializeField]
        string _blueprintData;

        static JsonSerializer serializer;
        static JsonBlueprintComponentConverter jsonBlueprintComponentConverter;

        static JsonBlueprintScriptableObject ()
        {
            jsonBlueprintComponentConverter = new JsonBlueprintComponentConverter ();
            serializer = new JsonSerializer ();
            serializer.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            serializer.Converters.Add (jsonBlueprintComponentConverter);
        }

        public override void SetBlueprint (Blueprint blueprint)
        {
            if (blueprint == null)
                return;

            var components = new List<JsonComponentBlueprint> ();
            foreach (var component in blueprint.components)
            {
                components.Add (new JsonComponentBlueprint (component.index, component.value));
            }

            var jsonBlueprint = new JsonBlueprint (blueprint.context, blueprint.name, components.ToArray ());

            using (var stream = new StringWriter ())
            {
                serializer.Serialize (stream, jsonBlueprint);
                _blueprintData = stream.ToString ();
            }
        }

        public override Blueprint GetBlueprint ()
        {
            if (string.IsNullOrEmpty (_blueprintData))
                return null;

            using (var stringReader = new StringReader (_blueprintData))
            {
                using (var jsonReader = new JsonTextReader (stringReader))
                {
                    var jsonBlueprint = serializer.Deserialize<JsonBlueprint> (jsonReader);

                    var components = new List<ComponentBlueprint> ();
                    foreach (var component in jsonBlueprint.components)
                    {
                        components.Add (new ComponentBlueprint (component.index, component.value));
                    }

                    var blueprint = new Blueprint (jsonBlueprint.context, jsonBlueprint.name, components.ToArray ());
                    return blueprint;
                }
            }
        }
    }
}