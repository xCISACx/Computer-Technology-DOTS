﻿using Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.Rendering;

public readonly partial struct AsteroidAspect : IAspect
{
    public readonly Entity Entity;
    private readonly RefRW<LocalTransform> transform;
    private readonly RefRO<AsteroidMovementProperties> asteroidMovementProperties;
    private readonly RefRW<HealthComponent> asteroidHealth;

    private float Speed => asteroidMovementProperties.ValueRO.Speed;

    public float3 Position
    {
        get => transform.ValueRO.Position;
        set => transform.ValueRW.Position = value;
    }
    
    public HealthComponent Health
    {
        get => asteroidHealth.ValueRO;
        set => asteroidHealth.ValueRW = value;
    }

    public void Move(float deltaTime, float halfDimensionsX, float halfDimensionsY)
    {
        // Move towards the center of the screen when far away from it and away from it when close
        var moveDir = (new float3(0, 0, 0) - transform.ValueRO.Position);

        float distanceToCenter = Vector3.Distance(transform.ValueRO.Position, new float3(0, 0, 0));
        
        //float distanceToCenter = math.length(moveDir);

        if (distanceToCenter > 2.0f)
        {
            Position += math.normalize(moveDir) * Speed * deltaTime;
        }

        /*if (distanceToCenter > 2.0f && distanceToCenter < halfDimensionsX)
        {
            Position += math.normalize(moveDir) * Speed * deltaTime;
        }
        else if (distanceToCenter >= halfDimensionsX)
        {
            Position -= math.normalize(moveDir) * Speed * deltaTime;
        }*/
    }
}