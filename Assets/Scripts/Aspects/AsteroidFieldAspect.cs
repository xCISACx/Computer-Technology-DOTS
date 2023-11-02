using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public readonly partial struct AsteroidFieldAspect : IAspect
{
    public readonly Entity Entity;
    
    private readonly RefRO<LocalTransform> _transform;

    private LocalTransform Transform => _transform.ValueRO;

    private readonly RefRO<AsteroidFieldProperties> _asteroidFieldProperties;
    private readonly RefRW<AsteroidFieldRandom> _asteroidFieldRandom;
    private readonly RefRW<WaveData> _waveData;
    
    [ReadOnly] public readonly DynamicBuffer<AsteroidBuffer> asteroidPrefabBuffer;

    private readonly RefRW<AsteroidSpawnPoints> _asteroidSpawnPoints;
    private readonly RefRW<AsteroidSpawnTimer> _asteroidSpawnTimer;
    
    public int NumberOfAsteroidsToSpawn => _asteroidFieldProperties.ValueRO.NumberOfAsteroidsToSpawn;

    public float ShipSafetyRadius => _asteroidFieldProperties.ValueRO.ShipSafetyRadius;
    
    public int Wave1Amount => _waveData.ValueRO.Wave1Amount;
    public int Wave2Amount => _waveData.ValueRO.Wave2Amount;
    public int Wave3Amount => _waveData.ValueRO.Wave3Amount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Entity GetRandomAsteroidPrefab()
    {
        var randomIndex = _asteroidFieldRandom.ValueRW.Value.NextInt(asteroidPrefabBuffer.Length);
        return asteroidPrefabBuffer[randomIndex].Value;
    }
    
    public bool AsteroidSpawnPointInitialized()
    {
        return _asteroidSpawnPoints.ValueRO.Value.IsCreated && AsteroidSpawnPointCount > 0;
    }

    private int AsteroidSpawnPointCount => _asteroidSpawnPoints.ValueRO.Value.Value.Value.Length;
    
    public LocalTransform GetRandomAsteroidTransform()
    {
        return new LocalTransform
        {
            Position = GetRandomPosition(MinCorner, MaxCorner),
            Rotation = GetRandomRotation(),
            Scale = GetRandomScale(0.5f)
        };
    }
    
    public LocalTransform GetRandomAsteroidTransformOffscreen()
    {
        return new LocalTransform
        {
            Position = new float3(-1000, 0, 0),
            Rotation = GetRandomRotation(),
            Scale = GetRandomScale(0.5f)
        };
    }
    
    public LocalTransform GetRandomAsteroidTransformCloseToBounds()
    {
        return new LocalTransform
        {
            Position = GetRandomPosition(MinCorner - 1f, MaxCorner + 1f),
            Rotation = GetRandomRotation(),
            Scale = GetRandomScale(0.5f)
        };
    }

    private float3 GetRandomPosition(float3 min, float3 max)
    {
        float3 randomPosition;
        do
        {
            randomPosition = _asteroidFieldRandom.ValueRW.Value.NextFloat3(min, max);
        }
        while (math.distancesq(Transform.Position, randomPosition) <= ShipSafetyRadius);

        return randomPosition;
    }
        
    private float3 MinCorner => Transform.Position - HalfDimensions;
    private float3 MaxCorner => Transform.Position + HalfDimensions;
    private float3 HalfDimensions => new()
    {
        x = _asteroidFieldProperties.ValueRO.FieldDimensions.x * 0.5f,
        y = _asteroidFieldProperties.ValueRO.FieldDimensions.y * 0.5f,
        z = 0f
    };

    private quaternion GetRandomRotation() => quaternion.RotateZ(_asteroidFieldRandom.ValueRW.Value.NextFloat(-0.25f, 0.25f));
    private float GetRandomScale(float min) => _asteroidFieldRandom.ValueRW.Value.NextFloat(min, 1f);
    
    private float3 GetRandomAsteroidSpawnPoint()
    {
        return GetAsteroidSpawnPoint(_asteroidFieldRandom.ValueRW.Value.NextInt(AsteroidSpawnPointCount));
    }

    private float3 GetAsteroidSpawnPoint(int i) => _asteroidSpawnPoints.ValueRO.Value.Value.Value[i];
}
