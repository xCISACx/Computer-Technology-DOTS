using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine.Rendering;

public readonly partial struct AsteroidAspect : IAspect
{
    public readonly Entity Entity;
    private readonly RefRW<LocalTransform> transform;
    private readonly RefRO<AsteroidMovementProperties> asteroidMovementProperties;

    private float Speed => asteroidMovementProperties.ValueRO.Speed;

    private float3 Position
    {
        get => transform.ValueRO.Position;
        set => transform.ValueRW.Position = value;
    }

    public void Move(float deltaTime)
    {
        // Move towards the center of the screen
        var moveDir = (new float3(0, 0, 0) - transform.ValueRO.Position);
        Position += moveDir * Speed * deltaTime;
    }
}