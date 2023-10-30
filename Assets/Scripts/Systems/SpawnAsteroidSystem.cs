using System.Collections.Generic;
using System.Linq;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
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
        var waveData = SystemAPI.GetComponent<WaveData>(asteroidFieldEntity);
        var asteroidField = SystemAPI.GetAspect<AsteroidFieldAspect>(asteroidFieldEntity);
        
        waveData.Wave1Amount = 50;
        waveData.Wave2Amount = 100;
        waveData.Wave3Amount = 150;

        var ecb = new EntityCommandBuffer(Allocator.Temp);

        var entityManager = state.EntityManager;
            
        var allAsteroidsBuffer = entityManager.GetBuffer<EntityBufferElement>(asteroidFieldEntity);
        
        var wave1AsteroidsBuffer = entityManager.GetBuffer<EntityBufferElement>(asteroidFieldEntity);
        /*var wave2AsteroidsBuffer = entityManager.GetBuffer<EntityBufferElement>(asteroidFieldEntity);
        var wave3AsteroidsBuffer = entityManager.GetBuffer<EntityBufferElement>(asteroidFieldEntity);*/

        var builder = new BlobBuilder(Allocator.Temp);
        ref var spawnPoints = ref builder.ConstructRoot<AsteroidSpawnPointsBlob>();
        var arrayBuilder = builder.Allocate(ref spawnPoints.Value, asteroidField.NumberOfAsteroidsToSpawn);
        
        for (var i = 0; i < asteroidField.NumberOfAsteroidsToSpawn; i++)
        {
            var newAsteroid = ecb.Instantiate(asteroidField.GetRandomAsteroidPrefab());
            LocalTransform offscreenPosition = asteroidField.GetRandomAsteroidTransformOffscreen();
            var newAsteroidTransform = offscreenPosition;

            ecb.SetComponent(newAsteroid, newAsteroidTransform);
    
            var newAsteroidSpawnPoint = newAsteroidTransform.Position;
            arrayBuilder[i] = newAsteroidSpawnPoint;

            allAsteroidsBuffer.Add(new EntityBufferElement { Value = newAsteroid });
        }

        for (var i = 0; i < waveData.Wave1Amount; i++)
        {
            wave1AsteroidsBuffer.Add(allAsteroidsBuffer[i]);
            allAsteroidsBuffer.RemoveAt(i);

            var newAsteroidTransform = asteroidField.GetRandomAsteroidTransform();
            ecb.SetComponent(wave1AsteroidsBuffer[i].Value, newAsteroidTransform);
        }
        
        var blobAsset = builder.CreateBlobAssetReference<AsteroidSpawnPointsBlob>(Allocator.Persistent);
        ecb.SetComponent(asteroidFieldEntity, new AsteroidSpawnPoints{Value = blobAsset});
        builder.Dispose();
        
        ecb.Playback(state.EntityManager);
    }
}