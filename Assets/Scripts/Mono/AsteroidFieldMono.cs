using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Random = Unity.Mathematics.Random;


public class AsteroidFieldMono : MonoBehaviour
{
    public float2 FieldDimensions;
    public int NumberOfAsteroidsToSpawn;
    public uint RandomSeed;
    public GameObject AsteroidPrefab;
    public float AsteroidSpawnRate;
}

public class AsteroidFieldBaker : Baker<AsteroidFieldMono>
{
    public override void Bake(AsteroidFieldMono authoring)
    {
        var asteroidFieldEntity = GetEntity(TransformUsageFlags.Dynamic);
        
        AddComponent(asteroidFieldEntity, new AsteroidFieldProperties
        {
            FieldDimensions = authoring.FieldDimensions,
            NumberOfAsteroidsToSpawn = authoring.NumberOfAsteroidsToSpawn,
            AsteroidPrefab = GetEntity(authoring.AsteroidPrefab, TransformUsageFlags.Dynamic),
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
