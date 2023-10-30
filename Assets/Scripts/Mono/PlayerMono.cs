using Unity.Entities;
using UnityEngine;

public class PlayerAuthoring : MonoBehaviour
{
    public float MovementSpeed;
    public float RotationSpeed = 0.5f;
    public GameObject ProjectilePrefab;
}

public class PlayerAuthoringBaker : Baker<PlayerAuthoring>
{
    public override void Bake(PlayerAuthoring authoring)
    {
        var playerEntity = GetEntity(TransformUsageFlags.Dynamic);
            
        AddComponent<PlayerTag>(playerEntity);
        AddComponent<PlayerMovementInput>(playerEntity);
            
        AddComponent<PlayerProjectileTag>(playerEntity);
        SetComponentEnabled<PlayerProjectileTag>(playerEntity, false);
            
        AddComponent(playerEntity, new PlayerMovementProperties
        {
            MovementSpeed = authoring.MovementSpeed,
            RotationSpeed = authoring.RotationSpeed
            
        });
        AddComponent(playerEntity, new ProjectilePrefab
        {
            Value = GetEntity(authoring.ProjectilePrefab, TransformUsageFlags.Dynamic)
        });
    }
}