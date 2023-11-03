using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

[BurstCompile]
public partial struct PlayerProjectileMovementSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var deltaTime = SystemAPI.Time.DeltaTime;

        foreach (var (transform, projectileProperties) in SystemAPI.Query<RefRW<LocalTransform>, PlayerProjectileProperties>())
        {
            transform.ValueRW.Position += transform.ValueRO.Up() * projectileProperties.MovementSpeed * deltaTime;
        }
    }
}