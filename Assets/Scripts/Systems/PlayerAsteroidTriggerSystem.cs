using Components;
using Unity.Entities;
using Unity.Collections;
using Unity.Physics;
using UnityEngine;

[UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
public partial struct PlayerAsteroidTriggerSystem : ISystem
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
            PlayerTag = SystemAPI.GetComponentLookup<PlayerTag>(isReadOnly: true),
            health = SystemAPI.GetComponentLookup<HealthComponent>(),
            //asteroidProperties = SystemAPI.GetComponentLookup<AsteroidProperties>(),
            Ecb = ecb
        };

        state.Dependency = j.Schedule(SystemAPI.GetSingleton<SimulationSingleton>(), state.Dependency);
        state.Dependency.Complete(); 
        
        ecb.Playback(state.EntityManager);
    }

    public partial struct ProcessTriggerEventsJob : ITriggerEventsJob
    {
        [ReadOnly] public ComponentLookup<AsteroidTag> AsteroidTag;
        [ReadOnly] public ComponentLookup<PlayerTag> PlayerTag;
        public ComponentLookup<HealthComponent> health;
        //public ComponentLookup<AsteroidProperties> asteroidProperties;
        public EntityCommandBuffer Ecb;

        public void Execute(Unity.Physics.TriggerEvent ce)
        {
            var entityA = ce.EntityA;
            var entityB = ce.EntityB;

            if (AsteroidTag.HasComponent(entityA) && PlayerTag.HasComponent(entityB))
            {
                Debug.Log("Player collided with asteroid A");

                var modifiedPlayerHealth = health[entityB];
                
                //TODO: change asteroid contact damage based on scale
                //modifiedPlayerHealth.Value -= 1;

                if (modifiedPlayerHealth.Value <= 0)
                {
                    modifiedPlayerHealth.IsDead = true;   
                }
                health[entityB] = modifiedPlayerHealth;
            }
            
            if (AsteroidTag.HasComponent(entityB) && PlayerTag.HasComponent(entityA))
            {
                Debug.Log("Player collided with asteroid B");
                
                var modifiedPlayerHealth = health[entityA];
                
                //TODO: change asteroid contact damage based on scale
                //modifiedPlayerHealth.Value -= 1;

                if (modifiedPlayerHealth.Value <= 0)
                {
                    modifiedPlayerHealth.IsDead = true;   
                }
                health[entityA] = modifiedPlayerHealth;
            }
        }
    }
}