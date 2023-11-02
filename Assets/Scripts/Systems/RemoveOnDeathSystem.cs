using Components;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Transforms;
using UnityEngine;

[UpdateBefore(typeof(TransformSystemGroup))]
public partial class RemoveOnDeathSystem : SystemBase
{
    protected override void OnUpdate()
    {
        var asteroidFieldEntity = SystemAPI.GetSingletonEntity<AsteroidFieldProperties>();
        var asteroidField = SystemAPI.GetAspect<AsteroidFieldAspect>(asteroidFieldEntity);
        
        var ecb = new EntityCommandBuffer(Allocator.TempJob);

        Entities.WithAny<PlayerTag, PlayerProjectileTriggerTag, AsteroidTag>()
            .ForEach((Entity entity, in HealthComponent health) =>
            {
                if (health.IsDead)
                {
                    LocalTransform newTransform = asteroidField.GetRandomAsteroidTransformCloseToBounds();
                    HealthComponent newHealth = new HealthComponent()
                    {
                        CurrentValue = Mathf.CeilToInt(newTransform.Scale * 3),
                        IsDead = false
                    };
                    ecb.SetComponent(entity, newTransform);
                    ecb.SetComponent(entity, newHealth);
                    //ecb.DestroyEntity(entity);
                }
            }).Run();
        
        ecb.Playback(EntityManager);
        ecb.Dispose();
    }
}