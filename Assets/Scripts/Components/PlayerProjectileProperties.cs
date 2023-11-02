using Unity.Entities;

public struct PlayerProjectileProperties : IComponentData
{
    public float MovementSpeed;
    public int Damage;
}