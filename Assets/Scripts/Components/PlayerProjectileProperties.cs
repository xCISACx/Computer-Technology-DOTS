using Unity.Entities;

public struct PlayerProjectileProperties : IComponentData
{
    public Entity Entity;
    public float MovementSpeed;
    public int Damage;
    public float TimeRemaining;
}