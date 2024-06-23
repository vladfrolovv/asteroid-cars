using Core.Runtime.Base;
using UnityEngine;
namespace Game.Runtime.Asteroids
{
    [CreateAssetMenu(fileName = "Asteroid Config", menuName = "SwyTapp/Game/Runtime/Asteroids/Asteroid Config")]
    public class AsteroidConfig : BaseScriptableObject
    {

        [SerializeField] private Vector2 _fallSpeed;
        [SerializeField] private Vector2 _fallDistance;

        public Vector2 FallSpeed => _fallSpeed;
        public Vector2 FallDistance => _fallDistance;

    }
}
