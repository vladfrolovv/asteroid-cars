#region

using System.Collections;
using Core.Runtime.Base;
using Game.Runtime.UtilitiesContainer;
using UnityEngine;

#endregion

namespace Game.Runtime.Planets
{
    public class PlanetLifecycle : BaseBehaviour
    {
        [SerializeField] private float _planetLifetime = 60f;

        [SerializeField] private Vector3 _standardScale = Vector3.one * 40f;
        [SerializeField] private Vector3 _minScale = Vector3.one * 10f;

        private IEnumerator _planetLifecycleRoutine;


        protected void OnEnable()
        {
            StartCoroutine(Utilities.Animate(_planetLifetime, null,
                Easings.EaseOutQuint(Utilities.Scale(gameObject, _standardScale, _minScale))));
        }

    }
}
