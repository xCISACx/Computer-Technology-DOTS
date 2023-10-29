using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct AsteroidFieldProperties : IComponentData
{
    public float2 FieldDimensions;
    public int NumberOfAsteroidsToSpawn;
    public float AsteroidSpawnRate;
}

public struct AsteroidSpawnTimer : IComponentData
{
    public float Value;
}

[InternalBufferCapacity(10)]
public struct AsteroidBuffer : IBufferElementData
{
    public Entity Value;
}

[InternalBufferCapacity(150)]
public struct Wave1Asteroids : IBufferElementData
{
    public Entity Value;
}

[InternalBufferCapacity(150)]
public struct Wave2Asteroids : IBufferElementData
{
    public Entity Value;
}

[InternalBufferCapacity(150)]
public struct Wave3Asteroids : IBufferElementData
{
    public Entity Value;
}
