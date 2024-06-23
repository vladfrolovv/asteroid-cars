#region

using Core.Runtime.Base;
using UnityEngine;
using Zenject;

#endregion

namespace Game.Runtime.Ids
{
    public class IdRecorder : BaseBehaviour
    {
        [SerializeField] private IdAsset _id;

        private IdContainer _idContainer;

        protected void Start()
        {
            _idContainer.Add(_id, gameObject);
        }

        protected void OnDestroy()
        {
            _idContainer.Remove(_id);
        }

        [Inject]
        public void Construct(IdContainer idContainer)
        {
            _idContainer = idContainer;
        }
    }
}
