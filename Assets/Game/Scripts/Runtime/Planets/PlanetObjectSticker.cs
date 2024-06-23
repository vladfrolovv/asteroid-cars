#region

using Core.Runtime.Base;
using Game.Runtime.Ids;
using Game.Runtime.UtilitiesContainer;
using UnityEngine;
using Zenject;

#endregion

namespace Game.Runtime.Planets
{
    public class PlanetObjectSticker : BaseBehaviour
    {

        [SerializeField] private IdAsset _planetId;

        private IdContainer _idContainer;
        private Vector3 _directionToPlanet;

        private void OnEnable()
        {
            StartCoroutine(Utilities.Wait(() =>
            {
                GameObject planet = _idContainer.Get(_planetId);
                _directionToPlanet = (planet.transform.position - transform.position).normalized;
            }));
        }

        protected void FixedUpdate()
        {
            Physics.Raycast(transform.position, _directionToPlanet, out RaycastHit hit, 100f);
            if (hit.collider != null)
            {
                transform.position = hit.point;
            }
        }


        [Inject]
        public void Construct(IdContainer idContainer)
        {
            _idContainer = idContainer;
        }

    }
}
