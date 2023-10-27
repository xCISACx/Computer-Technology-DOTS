using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public struct AsteroidSpawnPoints : IComponentData
{
    public BlobAssetReference<AsteroidSpawnPointsBlob> Value;
}

public struct AsteroidSpawnPointsBlob
{
    public BlobArray<float3> Value;
}
