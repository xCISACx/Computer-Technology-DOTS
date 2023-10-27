using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[BurstCompile]
[UpdateInGroup(typeof(InitializationSystemGroup))]
public partial struct SpawnAsteroidSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<AsteroidFieldProperties>();
    }

    [BurstCompile]
    public void OnDestroy(ref SystemState state)
    {
        
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        state.Enabled = false;
        var asteroidFieldEntity = SystemAPI.GetSingletonEntity<AsteroidFieldProperties>();
        var asteroidField = SystemAPI.GetAspect<AsteroidFieldAspect>(asteroidFieldEntity);

        var ecb = new EntityCommandBuffer(Allocator.Temp);
        var asteroidOffset = new float3(0f, -2f, 1f);
        
        var builder = new BlobBuilder(Allocator.Temp);
        ref var spawnPoints = ref builder.ConstructRoot<AsteroidSpawnPointsBlob>();
        var arrayBuilder = builder.Allocate(ref spawnPoints.Value, asteroidField.NumberOfAsteroidsToSpawn);
        
        for (var i = 0; i < asteroidField.NumberOfAsteroidsToSpawn; i++)
        {
            var newAsteroid = ecb.Instantiate(asteroidField.GetRandomAsteroidPrefab());
            var newAsteroidTransform = asteroidField.GetRandomAsteroidTransform();
            ecb.SetComponent(newAsteroid, newAsteroidTransform);
    
            var newAsteroidSpawnPoint = newAsteroidTransform.Position + asteroidOffset;
            arrayBuilder[i] = newAsteroidSpawnPoint;
        }
        
        var blobAsset = builder.CreateBlobAssetReference<AsteroidSpawnPointsBlob>(Allocator.Persistent);
        ecb.SetComponent(asteroidFieldEntity, new AsteroidSpawnPoints{Value = blobAsset});
        builder.Dispose();
        
        ecb.Playback(state.EntityManager);
    }
}