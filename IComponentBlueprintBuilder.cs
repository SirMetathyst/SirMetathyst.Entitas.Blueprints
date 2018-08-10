using Entitas;

namespace SirMetathyst.Entitas.Blueprints
{
    public interface IComponentBlueprintBuilder
    {
        ComponentBlueprint CreateComponentBlueprint (int index, IComponent component);
    }
}