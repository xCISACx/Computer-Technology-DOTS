using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEditor.SceneManagement;
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

        /*var waveBuffer = AddBuffer<IntBufferElement>(asteroidFieldEntity);

        foreach (var amount in authoring.WaveAmounts)
        {
            waveBuffer.Add(new IntBufferElement() { Value = amount });
        }*/
        
        //buffer.ResizeUninitialized(authoring.AsteroidPrefabs.Length);

        //NativeArray<int> waveAmounts = new NativeArray<int>(authoring.WaveAmounts.ToArray(), Allocator.Temp);
        
        /*var waveArray = authoring.WaveAmounts.ToArray();
        NativeArray<int> waveAmounts = new NativeArray<int>(waveArray, Allocator.Temp);*/

        for (int i = 0; i < authoring.AsteroidPrefabs.Length; i++)
        {
            buffer.Add(new AsteroidBuffer
                { Value = GetEntity(authoring.AsteroidPrefabs[i], TransformUsageFlags.Dynamic) });
        }
        
        /*WaveData waveData = new WaveData
        {
            Wave1Amount = waveAmounts[0],
            Wave2Amount = waveAmounts[1],
            Wave3Amount = waveAmounts[2]
        };*/

        /*WaveData waveData = new WaveData
        {
            WaveAmounts = waveBuffer
        };

        Debug.Log(waveData.WaveAmounts);*/

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
        //AddComponent(asteroidFieldEntity, waveData);
        AddComponent<EntityBufferElement>(asteroidFieldEntity);
    }
}
