using Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Transforms;
using UnityEngine;

[BurstCompile]
[UpdateBefore(typeof(TransformSystemGroup))]
public partial class RemoveOnDeathSystem : SystemBase
{
    [BurstCompile]
    protected override void OnUpdate()
    {
        var asteroidFieldEntity = SystemAPI.GetSingletonEntity<AsteroidFieldProperties>();
        var asteroidField = SystemAPI.GetAspect<AsteroidFieldAspect>(asteroidFieldEntity);
        
        var ecb = new EntityCommandBuffer(Allocator.TempJob);

        Entities.WithAny<AsteroidTag>()
            .ForEach((Entity entity, in HealthComponent health) =>
            {
                if (health.IsDead)
                {
                    LocalTransform newTransform = asteroidField.GetRandomAsteroidTransformCloseToBounds();
                    HealthComponent newHealth = new HealthComponent()
                    {
                        Value = Mathf.CeilToInt(newTransform.Scale * 3),
                        IsDead = false
                    };
                    ecb.SetComponent(entity, newTransform);
                    ecb.SetComponent(entity, newHealth);
                }
            }).Run();
        
        Entities.WithAny<PlayerTag, PlayerProjectileTriggerTag>()
            .ForEach((Entity entity, in HealthComponent health) =>
            {
                if (health.IsDead)
                {
                    ecb.DestroyEntity(entity);
                }
            }).Run();
        
        ecb.Playback(EntityManager);
        ecb.Dispose();
    }
}