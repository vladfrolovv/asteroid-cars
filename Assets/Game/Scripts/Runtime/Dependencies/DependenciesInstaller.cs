#region

using Game.Runtime.Asteroids;
using Game.Runtime.Ids;
using UnityEngine;
using Zenject;

#endregion

namespace Game.Runtime.Dependencies
{
    public class DependenciesInstaller : MonoInstaller
    {

        [SerializeField] private IdContainer _idContainer;
        [SerializeField] private Asteroid _asteroidPrefab;


        public override void InstallBindings()
        {
            BindInstance(_idContainer);

            Container.BindFactory<AsteroidInfo, Asteroid, AsteroidsFactory>()
                .FromPoolableMemoryPool<AsteroidInfo, Asteroid, AsteroidsPool>(x =>
                    x.WithInitialSize(10).FromComponentInNewPrefab(_asteroidPrefab));
        }


        private void BindInstance<T>(T instance)
        {
            Container.Bind<T>().FromInstance(instance).AsSingle();
        }


        private class AsteroidsPool : MonoPoolableMemoryPool<AsteroidInfo, IMemoryPool, Asteroid>
        {
        }

    }
}
