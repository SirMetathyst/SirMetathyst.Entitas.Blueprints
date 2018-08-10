using System.Collections.Generic;

namespace SirMetathyst.Entitas.Blueprints
{
    public interface IBlueprintProvider
    {
        IList<Blueprint> GetBlueprintList ();
    }
}