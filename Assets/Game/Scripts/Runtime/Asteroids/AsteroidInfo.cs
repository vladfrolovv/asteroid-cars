#region

using UnityEngine;

#endregion

namespace Game.Runtime.Asteroids
{
    public class AsteroidInfo
    {

        public AsteroidInfo(GameObject prefab, Vector3 position, Vector3 normal, float distance, float fallSpeed)
        {
            Prefab = prefab;
            Position = position;
            Normal = normal;
            Distance = distance;
            FallSpeed = fallSpeed;
        }


        public GameObject Prefab { get; }
        public Vector3 Position { get; }
        public Vector3 Normal { get; }
        public float Distance { get; }
        public float FallSpeed { get; }
    }
}
