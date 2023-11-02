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
        //state.RequireForUpdate<SimulationSingleton>();
    }

    public void OnUpdate(ref SystemState state)
    {
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

        public void Execute(Unity.Physics.TriggerEvent ce)
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
                modifiedHealth.Value -= projectileProperties[entityB].Damage;
                Debug.Log(modifiedHealth.Value);
                if (modifiedHealth.Value <= 0)
                {
                    
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
                modifiedHealth.Value -= projectileProperties[entityA].Damage;
                Debug.Log(modifiedHealth.Value);
                if (modifiedHealth.Value <= 0)
                {
                    Debug.Log(modifiedHealth.Value);
                    modifiedHealth.IsDead = true;   
                }
                health[entityB] = modifiedHealth;
            }
        }
    }
}