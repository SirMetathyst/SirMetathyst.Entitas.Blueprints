using Entitas;

namespace SirMetathyst.Entitas.Blueprints
{
    public interface IBlueprintBuilder
    {
        Blueprint CreateBlueprint (IEntity entity, string name);
    }
}