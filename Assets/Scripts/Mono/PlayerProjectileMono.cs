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
    public float TimeRemaining;

    public class ProjectileMoveSpeedBaker : Baker<PlayerProjectileMono>
    {
        public override void Bake(PlayerProjectileMono authoring)
        {
            var projectileEntity = GetEntity(TransformUsageFlags.Dynamic);
            
            AddComponent(projectileEntity, new PlayerProjectileProperties()
            {
                Entity = projectileEntity,
                MovementSpeed = authoring.ProjectileMovementSpeed,
                Damage = authoring.ProjectileDamage,
                TimeRemaining = authoring.TimeRemaining
            });
            AddComponent<PlayerProjectileTriggerTag>(projectileEntity);
            AddComponent(projectileEntity, new HealthComponent()
            {
                Value = authoring.Health,
                IsDead = authoring.IsDead
            });
        }
    }
}
