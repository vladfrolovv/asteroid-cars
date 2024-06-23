#region

using System.Collections;
using System.Collections.Generic;
using Core.Runtime.Base;
using Game.Runtime.Ids;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

#endregion

namespace Game.Runtime.Asteroids
{
    public class AsteroidsRain : BaseBehaviour
    {

        [SerializeField] private Transform _parent;
        [SerializeField] private Transform _planetTransform;
        [SerializeField] private float _spawnInterval;
        [SerializeField] private List<GameObject> _asteroidsVariants = new List<GameObject>();

        private AsteroidsFactory _asteroidsFactory;
        private IdContainer _idContainer;
        private AsteroidConfig _asteroidConfig;
        private IEnumerator _asteroidsRainRoutine;

        protected void OnEnable()
        {
            StartCoroutine(_asteroidsRainRoutine = AsteroidsRoutine());
        }

        protected void OnDisable()
        {
            if (_asteroidsRainRoutine != null)
                StopCoroutine(_asteroidsRainRoutine);
        }

        private IEnumerator AsteroidsRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(_spawnInterval);
                SpawnAsteroid();
            }
        }

        private void SpawnAsteroid()
        {
            GameObject prefab = _asteroidsVariants[Random.Range(0, _asteroidsVariants.Count)];
            Vector3 position = GetRandomPositionOnSphere();
            Vector3 normal = GetNormalOfPosition(position);

            float distance = Random.Range(_asteroidConfig.FallDistance.x, _asteroidConfig.FallDistance.y);
            float fallSpeed = Random.Range(_asteroidConfig.FallSpeed.x, _asteroidConfig.FallSpeed.y);

            AsteroidInfo asteroidInfo = new AsteroidInfo(prefab, position, normal, distance, fallSpeed);
            Asteroid asteroid = _asteroidsFactory.Create(asteroidInfo);

            asteroid.transform.parent = _parent;
        }

        private Vector3 GetRandomPositionOnSphere()
        {
            return Random.onUnitSphere * _planetTransform.localScale.x;
        }

        private Vector3 GetNormalOfPosition(Vector3 fromPosition)
        {
            Vector3 basePosition = _planetTransform.position;
            return (fromPosition - basePosition).normalized;
        }

        [Inject]
        public void Construct(AsteroidsFactory asteroidsFactory, IdContainer idContainer, AsteroidConfig asteroidConfig)
        {
            _asteroidsFactory = asteroidsFactory;
            _asteroidConfig = asteroidConfig;
            _idContainer = idContainer;
        }
    }
}
