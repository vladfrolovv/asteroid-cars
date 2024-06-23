#region

using System.Collections.Generic;
using Core.Runtime.Base;
using UnityEngine;

#endregion

namespace Game.Runtime.Ids
{
    public class IdContainer : BaseBehaviour
    {

        private readonly Dictionary<IdAsset, GameObject> _idObjectsMap
            = new Dictionary<IdAsset, GameObject>();

        public void Add(IdAsset id, GameObject go)
        {
            _idObjectsMap.Add(id, go);
        }

        public void Remove(IdAsset id)
        {
            _idObjectsMap.Remove(id);
        }

        public GameObject Get(IdAsset id)
        {
            return _idObjectsMap[id];
        }
    }
}
