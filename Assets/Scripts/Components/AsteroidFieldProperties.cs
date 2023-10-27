using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct AsteroidFieldProperties : IComponentData
{
    public float2 FieldDimensions;
    public int NumberOfAsteroidsToSpawn;
    public Entity AsteroidPrefab;
    public float AsteroidSpawnRate;
}

public struct AsteroidSpawnTimer : IComponentData
{
    public float Value;
}
