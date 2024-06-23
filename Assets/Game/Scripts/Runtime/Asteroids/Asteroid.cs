#region

using System;
using System.Collections;
using Core.Runtime.Base;
using Game.Runtime.UtilitiesContainer;
using UnityEngine;
using Zenject;

#endregion

namespace Game.Runtime.Asteroids
{
    public class Asteroid : BaseBehaviour, IPoolable<AsteroidInfo, IMemoryPool>, IDisposable
    {
        [SerializeField] private Transform _targetTransform;
        [SerializeField] private Transform _modelTransform;

        private IMemoryPool _pool;
        private AsteroidInfo _info;
        private IEnumerator _asteroidFallingRoutine;

        public void Dispose()
        {
            _pool?.Despawn(this);
        }

        void IPoolable<AsteroidInfo, IMemoryPool>.OnSpawned(AsteroidInfo info, IMemoryPool pool)
        {
            _info = info;
            _pool = pool;

            InstallAsteroid();
        }

        void IPoolable<AsteroidInfo, IMemoryPool>.OnDespawned()
        {
            _pool = null;
            _info = null;

            if (_asteroidFallingRoutine != null)
                StopCoroutine(_asteroidFallingRoutine);
        }

        private void InstallAsteroid()
        {
            _targetTransform.localPosition = _info.Position;
            _targetTransform.rotation = Quaternion.FromToRotation(Vector3.forward, _info.Normal);
            _modelTransform.localPosition = _info.Normal * _info.Distance;

            Instantiate(_info.Prefab, _modelTransform);
            StartCoroutine(_asteroidFallingRoutine = AsteroidFallingRoutine());
        }

        private IEnumerator AsteroidFallingRoutine()
        {
            while (Vector3.Distance(_modelTransform.localPosition, _targetTransform.localPosition) > .01f)
            {
                _modelTransform.localPosition = Vector3.MoveTowards(_modelTransform.localPosition,
                    _targetTransform.localPosition, _info.FallSpeed * Time.deltaTime);

                yield return null;
            }
        }

    }
}
