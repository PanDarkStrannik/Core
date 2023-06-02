using GameCore.Patterns;

namespace GameCore.Systems.ConditionSystem
{
    public interface IConditionData : IData
    {
        public bool NeedRefreshAfterPassed { get; }
    }
}
