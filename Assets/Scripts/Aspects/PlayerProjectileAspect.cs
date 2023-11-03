using Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.Rendering;

public readonly partial struct PlayerProjectileAspect : IAspect
{
    public readonly Entity Entity;
    private readonly RefRW<LocalTransform> transform;
    private readonly RefRW<PlayerProjectileProperties> playerProjectileProperties;
    private readonly RefRW<HealthComponent> playerProjectileHealth;

    private float MovementSpeed => playerProjectileProperties.ValueRO.MovementSpeed;

    public float3 Position
    {
        get => transform.ValueRO.Position;
        set => transform.ValueRW.Position = value;
    }

    public HealthComponent Health
    {
        get => playerProjectileHealth.ValueRO;
        set => playerProjectileHealth.ValueRW = value;
    }

    public int Damage
    {
        get => playerProjectileProperties.ValueRO.Damage;
        set => playerProjectileProperties.ValueRW.Damage = value;
    }
}