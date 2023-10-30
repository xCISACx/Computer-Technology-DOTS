using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public class AsteroidMono : MonoBehaviour
{
    public float Speed;
}

public class AsteroidBaker : Baker<AsteroidMono>
{
    public override void Bake(AsteroidMono authoring)
    {
        var asteroidEntity = GetEntity(TransformUsageFlags.Dynamic);

        AddComponent(asteroidEntity, new AsteroidMovementProperties()
        {
            Speed = authoring.Speed
        });

        //AddComponent<AsteroidAspect>(asteroidEntity);
    }
}