using Components;
using Unity.Entities;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerProjectileMono : MonoBehaviour
{
    public int Health;
    public bool IsDead;
    public float ProjectileMovementSpeed;
    public int ProjectileDamage;

    public class ProjectileMoveSpeedBaker : Baker<PlayerProjectileMono>
    {
        public override void Bake(PlayerProjectileMono authoring)
        {
            var projectileEntity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(projectileEntity, new PlayerProjectileProperties()
            {
                MovementSpeed = authoring.ProjectileMovementSpeed,
                Damage = authoring.ProjectileDamage
            });
            AddComponent<PlayerProjectileTriggerTag>(projectileEntity);
            AddComponent(projectileEntity, new HealthComponent()
            {
                CurrentValue = authoring.Health,
                IsDead = authoring.IsDead
            });
        }
    }
}
