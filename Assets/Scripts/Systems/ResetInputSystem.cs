using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

[BurstCompile]
[UpdateInGroup(typeof(SimulationSystemGroup), OrderLast = true)]
[UpdateAfter(typeof(EndSimulationEntityCommandBufferSystem))]
public partial struct ResetInputSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var ecb = new EntityCommandBuffer(Allocator.Temp);
            
        foreach (var (tag, entity) in SystemAPI.Query<PlayerProjectileTag>().WithEntityAccess())
        {
            ecb.SetComponentEnabled<PlayerProjectileTag>(entity, false);
        }
            
        ecb.Playback(state.EntityManager);
        ecb.Dispose();
    }
}