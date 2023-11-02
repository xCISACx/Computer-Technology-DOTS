using Unity.Entities;

namespace Components
{
    public partial struct HealthComponent : IComponentData
    {
        public int Value;
        public bool IsDead;
    }
}