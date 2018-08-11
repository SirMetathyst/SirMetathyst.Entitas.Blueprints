using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace SirMetathyst.Entitas.Blueprints.Unity
{
    [CreateAssetMenu (menuName = "Entitas/Binary Blueprint", fileName = "New Binary Blueprint.asset")]
    public class BinaryBlueprintScriptableObject : BlueprintScriptableObject
    {
        [SerializeField]
        byte[] _blueprintData;

        BinaryFormatter formatter = new BinaryFormatter ();

        public override void SetBlueprint (Blueprint blueprint)
        {
            if (blueprint == null)
                return;

            using (var stream = new MemoryStream ())
            {
                formatter.Serialize (stream, blueprint);
                _blueprintData = stream.ToArray ();
            }
        }

        public override Blueprint GetBlueprint ()
        {
            if (_blueprintData == null || _blueprintData.Length <= 0)
                return null;

            using (var stream = new MemoryStream (_blueprintData))
            {
                return (Blueprint) formatter.Deserialize (stream);
            }
        }
    }
}