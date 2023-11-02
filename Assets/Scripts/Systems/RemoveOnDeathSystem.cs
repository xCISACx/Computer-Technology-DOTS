using Components;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Transforms;

[UpdateBefore(typeof(TransformSystemGroup))]
public partial class RemoveOnDeathSystem : SystemBase
{
    protected override void OnUpdate()
    {
        var ecb = new EntityCommandBuffer(Allocator.TempJob);

        Entities.WithAny<PlayerTag, PlayerProjectileTriggerTag, AsteroidTag>()
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