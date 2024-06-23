using Game.Runtime.Asteroids;
using UnityEngine;
using Zenject;
namespace Game.Runtime.Dependencies
{
    [CreateAssetMenu(fileName = "Configs", menuName = "SwyTapp/Game/Runtime/Configs")]
    public class ConfigsInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private AsteroidConfig _asteroidConfig;

        public override void InstallBindings()
        {
            Container.BindInstance(_asteroidConfig);
        }

    }
}
