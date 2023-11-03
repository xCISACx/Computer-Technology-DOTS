using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;

[BurstCompile]
public partial struct AsteroidMovementSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<AsteroidMovementProperties>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var asteroidFieldEntity = SystemAPI.GetSingletonEntity<AsteroidFieldProperties>();
        var asteroidField = SystemAPI.GetAspect<AsteroidFieldAspect>(asteroidFieldEntity);

        var halfDimensionsX = asteroidField.HalfDimensions.x;
        var halfDimensionsY = asteroidField.HalfDimensions.y;
        
        var deltaTime = SystemAPI.Time.DeltaTime;

        new AsteroidMovementJob
        {
            DeltaTime = deltaTime,
            HalfDimensionsX = halfDimensionsX,
            HalfDimensionsY = halfDimensionsY,
        }.ScheduleParallel();
    }
    
    [BurstCompile]
    public partial struct AsteroidMovementJob : IJobEntity
    {
        public float DeltaTime;
        public float HalfDimensionsX;
        public float HalfDimensionsY;

        [BurstCompile]
        private void Execute(AsteroidAspect asteroid)
        {
            asteroid.Move(DeltaTime, HalfDimensionsX, HalfDimensionsY);
        }
    }
}