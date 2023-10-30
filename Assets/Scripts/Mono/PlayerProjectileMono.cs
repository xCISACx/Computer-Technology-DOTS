using Unity.Entities;
using UnityEngine;

public class PlayerProjectileMono : MonoBehaviour
{
    public float ProjectileMoveSpeed;

    public class ProjectileMoveSpeedBaker : Baker<PlayerProjectileMono>
    {
        public override void Bake(PlayerProjectileMono authoring)
        {
            var projectileEntity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(projectileEntity, new PlayerProjectileMovementSpeed { Value = authoring.ProjectileMoveSpeed });
        }
    }
}
