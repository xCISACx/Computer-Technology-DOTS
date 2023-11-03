using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Unity.Physics.Systems;
using Unity.Physics;
using Unity.Collections;

[BurstCompile]
[UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
public partial class DestroyPlayerProjectileAfterTimeSystem : SystemBase
{
    [BurstCompile]
    protected override void OnUpdate()
    {
        float deltaTime = SystemAPI.Time.DeltaTime;
        
        var ecb = new EntityCommandBuffer(Allocator.TempJob);

        Entities
            .WithAll<PlayerProjectileTriggerTag>()
            .ForEach((Entity entity, ref PlayerProjectileProperties projectileProperties) =>
            {
                projectileProperties.TimeRemaining -= deltaTime;

                if (projectileProperties.TimeRemaining <= 0f)
                {
                    ecb.DestroyEntity(entity);
                }
            }).Run();
        
        ecb.Playback(EntityManager);
        ecb.Dispose();
    }
}