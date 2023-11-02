using Unity.Entities;

namespace Components
{
    public partial struct HealthComponent : IComponentData
    {
        public int CurrentValue;
        public bool IsDead;
    }
}