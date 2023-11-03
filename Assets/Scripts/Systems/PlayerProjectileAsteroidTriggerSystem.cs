using Unity.Burst;
using Unity.Entities;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Physics;
using UnityEngine;

[BurstCompile]
//[UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
public partial struct PlayerProjectileAsteroidTriggerSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<SimulationSingleton>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var ecb = new EntityCommandBuffer(Allocator.TempJob);

        foreach (var playerProjectile in SystemAPI.Query<PlayerProjectileAspect>().WithAll<PlayerProjectileTriggerTag>())
        {
            foreach (var asteroid
                     in SystemAPI.Query<AsteroidAspect>().WithAll<AsteroidTag>())
            {
                //var buffer = SystemAPI.GetSingleton<EndVariableRateSimulationEntityCommandBufferSystem.Singleton>();

                if (math.distance(playerProjectile.Position.x, asteroid.Position.x) < 0.3f &&
                    math.distance(playerProjectile.Position.y, asteroid.Position.y) < 0.3f)
                {
                    var modifiedProjectileHealth = playerProjectile.Health;
                    modifiedProjectileHealth.IsDead = true;
                    playerProjectile.Health = modifiedProjectileHealth;

                    var modifiedHealth = asteroid.Health;
                    modifiedHealth.Value -= playerProjectile.Damage;
                    //Debug.Log(modifiedHealth.Value);
                    if (modifiedHealth.Value <= 0)
                    {
                    
                        modifiedHealth.IsDead = true;   
                    }
                    asteroid.Health = modifiedHealth;
                    
                    //playerProjectile.DestroyAsteroid(buffer.CreateCommandBuffer(state.WorldUnmanaged), playerProjectile.Entity);
                    //asteroidAspect.DestroyAsteroid(buffer.CreateCommandBuffer(state.WorldUnmanaged), asteroidAspect.Entity);
                }
            }
        }

        /*var j = new ProcessTriggerEventsJob {
            AsteroidTag = SystemAPI.GetComponentLookup<AsteroidTag>(isReadOnly: true),
            PlayerProjectileTriggerTag = SystemAPI.GetComponentLookup<PlayerProjectileTriggerTag>(isReadOnly: true),
            health = SystemAPI.GetComponentLookup<HealthComponent>(),
            projectileProperties = SystemAPI.GetComponentLookup<PlayerProjectileProperties>(true),
            Ecb = ecb
        };*/

        //state.Dependency = j.Schedule(SystemAPI.GetSingleton<SimulationSingleton>(), state.Dependency);
        //state.Dependency.Complete(); 
        
        //ecb.Playback(state.EntityManager);
        //ecb.Dispose();
    }

    //[BurstCompile]
    /*public partial struct ProcessTriggerEventsJob : ITriggerEventsJob
    {
        [ReadOnly] public ComponentLookup<AsteroidTag> AsteroidTag;
        [ReadOnly] public ComponentLookup<PlayerProjectileTriggerTag> PlayerProjectileTriggerTag;
        public ComponentLookup<HealthComponent> health;
        [ReadOnly] public ComponentLookup<PlayerProjectileProperties> projectileProperties;
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
    }*/
}