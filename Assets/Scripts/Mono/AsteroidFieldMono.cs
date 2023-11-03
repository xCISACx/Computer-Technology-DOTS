using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;
using static System.Int32;
using Random = Unity.Mathematics.Random;

[ChunkSerializable]
public class AsteroidFieldMono : MonoBehaviour
{
    public float2 FieldDimensions;
    public int NumberOfAsteroidsToSpawn;
    public float ShipSafetyRadius = 20f;
    public List<int> WaveAmounts = new List<int>(3)
    {
        50, 100, 150
    };
    public uint RandomSeed;
    public GameObject[] AsteroidPrefabs;
    public float AsteroidSpawnRate;
}

public class AsteroidFieldBaker : Baker<AsteroidFieldMono>
{
    public override void Bake(AsteroidFieldMono authoring)
    {
        authoring.RandomSeed = (uint) UnityEngine.Random.Range(0, MaxValue);
        
        var asteroidFieldEntity = GetEntity(TransformUsageFlags.Dynamic);
        
        var buffer = AddBuffer<AsteroidBuffer>(asteroidFieldEntity);
        
        //buffer.ResizeUninitialized(authoring.AsteroidPrefabs.Length);

        NativeArray<int> waveAmounts = new NativeArray<int>(authoring.WaveAmounts.ToArray(), Allocator.Temp);

        for (int i = 0; i < authoring.AsteroidPrefabs.Length; i++)
        {
            buffer.Add(new AsteroidBuffer
                { Value = GetEntity(authoring.AsteroidPrefabs[i], TransformUsageFlags.Dynamic) });
        }

        WaveData waveData = new WaveData
        {
            Wave1Amount = waveAmounts[0],
            Wave2Amount = waveAmounts[1],
            Wave3Amount = waveAmounts[2]
        };

        /*Debug.Log(waveAmounts[0]);
        Debug.Log(waveAmounts[1]);
        Debug.Log(waveAmounts[2]);*/
        
        AddComponent(asteroidFieldEntity, new AsteroidFieldProperties
        {
            FieldDimensions = authoring.FieldDimensions,
            NumberOfAsteroidsToSpawn = authoring.NumberOfAsteroidsToSpawn,
            ShipSafetyRadius = authoring.ShipSafetyRadius,
            AsteroidSpawnRate = authoring.AsteroidSpawnRate
        });

        AddComponent(asteroidFieldEntity, new AsteroidFieldRandom
        {
            Value = Random.CreateFromIndex(authoring.RandomSeed)
        });
        AddComponent<AsteroidSpawnPoints>(asteroidFieldEntity);
        AddComponent<AsteroidSpawnTimer>(asteroidFieldEntity);
        AddComponent(asteroidFieldEntity, waveData);
        AddComponent<EntityBufferElement>(asteroidFieldEntity);
    }
}
