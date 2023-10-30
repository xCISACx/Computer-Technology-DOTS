using Unity.Burst;
using Unity.Entities;

[BurstCompile]
public partial struct AsteroidMovementSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<AsteroidMovementProperties>();
    }

    [BurstCompile]
    public void OnDestroy(ref SystemState state)
    {
        
    }
    
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var deltaTime = SystemAPI.Time.DeltaTime;

        new AsteroidMovementJob
        {
            DeltaTime = deltaTime
        }.ScheduleParallel();
    }
    
    [BurstCompile]
    public partial struct AsteroidMovementJob : IJobEntity
    {
        public float DeltaTime;

        [BurstCompile]
        private void Execute(AsteroidAspect asteroid)
        {
            asteroid.Move(DeltaTime);
        }
    }
}