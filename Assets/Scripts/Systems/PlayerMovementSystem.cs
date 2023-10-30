using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[UpdateBefore(typeof(TransformSystemGroup))]
public partial struct PlayerMovementSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var deltaTime = SystemAPI.Time.DeltaTime;
        new PlayerMoveJob
        {
            DeltaTime = deltaTime
        }.Schedule();
    }
}

[BurstCompile]
public partial struct PlayerMoveJob : IJobEntity
{
    public float DeltaTime;
        
    [BurstCompile]
    private void Execute(ref LocalTransform transform, in PlayerMovementInput movementInput, PlayerMovementProperties movementProperties)
    {
        transform.Position.xy += movementInput.Value * movementProperties.MovementSpeed * DeltaTime;

        if (math.lengthsq(movementInput.Value) > float.Epsilon)
        {
            // TODO: FIX NOT WORKING
            // Calculate the rotation angle based on the movement direction and rotate
            float rotationAngle = math.atan2(-movementInput.Value.x, movementInput.Value.y);
            
            rotationAngle += movementProperties.RotationSpeed * DeltaTime;
            
            quaternion zRotation = quaternion.RotateZ(rotationAngle);
            
            transform.Rotation = zRotation;
        }
    }
}