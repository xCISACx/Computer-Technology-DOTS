using Components;
using Unity.Entities;
using Unity.Collections;
using Unity.Physics;
using UnityEngine;

[UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
public partial struct PlayerProjectileAsteroidTriggerSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<SimulationSingleton>();
    }

    public void OnUpdate(ref SystemState state)
    {
        //Debug.Log("projectile trigger system on update");
        var ecb = new EntityCommandBuffer(Allocator.TempJob);

        var j = new ProcessTriggerEventsJob {
            AsteroidTag = SystemAPI.GetComponentLookup<AsteroidTag>(isReadOnly: true),
            PlayerProjectileTriggerTag = SystemAPI.GetComponentLookup<PlayerProjectileTriggerTag>(isReadOnly: true),
            health = SystemAPI.GetComponentLookup<HealthComponent>(),
            projectileProperties = SystemAPI.GetComponentLookup<PlayerProjectileProperties>(),
            Ecb = ecb
        };

        state.Dependency = j.Schedule(SystemAPI.GetSingleton<SimulationSingleton>(), state.Dependency);
        state.Dependency.Complete(); 
        
        ecb.Playback(state.EntityManager);
    }

    public partial struct ProcessTriggerEventsJob : ITriggerEventsJob
    {
        [ReadOnly] public ComponentLookup<AsteroidTag> AsteroidTag;
        [ReadOnly] public ComponentLookup<PlayerProjectileTriggerTag> PlayerProjectileTriggerTag;
        public ComponentLookup<HealthComponent> health;
        public ComponentLookup<PlayerProjectileProperties> projectileProperties;
        public EntityCommandBuffer Ecb;

        public void Execute(TriggerEvent ce)
        {
            var entityA = ce.EntityA;
            var entityB = ce.EntityB;

            if (AsteroidTag.HasComponent(entityA) && PlayerProjectileTriggerTag.HasComponent(entityB))
            {
                Debug.Log("Projectile collided with asteroid A");

                var modifiedProjectileHealth = health[entityB];
                modifiedProjectileHealth.IsDead = true;
                health[entityB] = modifiedProjectileHealth;
                
                var modifiedHealth = health[entityA];
                modifiedHealth.CurrentValue -= projectileProperties[entityB].Damage;
                if (modifiedHealth.CurrentValue <= 0)
                {
                    Debug.Log(modifiedHealth.CurrentValue);
                    modifiedHealth.IsDead = true;   
                }
                health[entityA] = modifiedHealth;
            }
            
            if (AsteroidTag.HasComponent(entityB) && PlayerProjectileTriggerTag.HasComponent(entityA))
            {
                Debug.Log("Projectile collided with asteroid B");
                
                var modifiedProjectileHealth = health[entityA];
                modifiedProjectileHealth.IsDead = true;
                health[entityA] = modifiedProjectileHealth;
                
                var modifiedHealth = health[entityB];
                modifiedHealth.CurrentValue -= projectileProperties[entityA].Damage;
                if (modifiedHealth.CurrentValue <= 0)
                {
                    Debug.Log(modifiedHealth.CurrentValue);
                    modifiedHealth.IsDead = true;   
                }
                health[entityB] = modifiedHealth;
            }
        }
    }
}