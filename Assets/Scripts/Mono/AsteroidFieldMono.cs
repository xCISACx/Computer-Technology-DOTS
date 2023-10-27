using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEditor.SceneManagement;
using UnityEngine;
using Random = Unity.Mathematics.Random;


public class AsteroidFieldMono : MonoBehaviour
{
    public float2 FieldDimensions;
    public int NumberOfAsteroidsToSpawn;
    public uint RandomSeed;
    public GameObject[] AsteroidPrefabs;
    public float AsteroidSpawnRate;
}

public class AsteroidFieldBaker : Baker<AsteroidFieldMono>
{
    public override void Bake(AsteroidFieldMono authoring)
    {
        var asteroidFieldEntity = GetEntity(TransformUsageFlags.Dynamic);
        
        var buffer = AddBuffer<AsteroidBuffer>(asteroidFieldEntity);
        
        buffer.ResizeUninitialized(authoring.AsteroidPrefabs.Length);

        for (int i = 0; i < authoring.AsteroidPrefabs.Length; i++)
        {
            buffer.Add(new AsteroidBuffer{ Value = GetEntity(authoring.AsteroidPrefabs[i], TransformUsageFlags.Dynamic) });
        }
        
        AddComponent(asteroidFieldEntity, new AsteroidFieldProperties
        {
            FieldDimensions = authoring.FieldDimensions,
            NumberOfAsteroidsToSpawn = authoring.NumberOfAsteroidsToSpawn,
            AsteroidSpawnRate = authoring.AsteroidSpawnRate
        });

        AddComponent(asteroidFieldEntity, new AsteroidFieldRandom
        {
            Value = Random.CreateFromIndex(authoring.RandomSeed)
        });
        AddComponent<AsteroidSpawnPoints>(asteroidFieldEntity);
        AddComponent<AsteroidSpawnTimer>(asteroidFieldEntity);
    }
}
