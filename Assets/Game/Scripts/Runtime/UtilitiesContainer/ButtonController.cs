#region

using System.Collections;
using Core.Runtime.Base;
using UnityEngine;
using UnityEngine.UI;

#endregion

namespace Game.Runtime.UtilitiesContainer
{
    [RequireComponent(typeof(Button))]
    public class ButtonController : BaseBehaviour
    {
        private Button _button;

        protected void Awake()
        {
            _button = GetComponent<Button>();
        }


        protected void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
        }


        protected void OnDisable()
        {
            StopAllCoroutines();
            _button.onClick.RemoveListener(OnClick);
            _button.enabled = true;
        }


        private void OnClick()
        {
            if (!gameObject.activeSelf) return;
            StartCoroutine(ButtonLifecycleRoutine());
        }


        private IEnumerator ButtonLifecycleRoutine()
        {
            _button.enabled = false;
            yield return null;
            _button.enabled = true;
        }
    }
}
