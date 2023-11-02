using Unity.Entities;
using Unity.Mathematics;

public struct PlayerMovementInput : IComponentData
{
    public float2 Value;
}

public struct PlayerMovementProperties : IComponentData
{
    public float MovementSpeed;
    public float RotationSpeed;
}

public struct ProjectilePrefab : IComponentData
{
    public Entity Value;
}