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
        public EntityCommandBuffer Ecb;

        public void Execute(Unity.Physics.TriggerEvent ce)
        {
            var entityA = ce.EntityA;
            var entityB = ce.EntityB;

            if (AsteroidTag.HasComponent(entityA) && PlayerTag.HasComponent(entityB)
                || AsteroidTag.HasComponent(entityB) && PlayerTag.HasComponent(entityA))
            {
                Debug.LogWarning("Player collided with asteroid");
            }
        }
    }
}