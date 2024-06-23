#region

using Core.Runtime.Base;
using UnityEngine;

#endregion

namespace Game.Runtime.UtilitiesContainer
{
    public class FPSInstaller : BaseBehaviour
    {
        [SerializeField]
        private bool _max;

        [SerializeField]
        private int _fps = 60;

        [SerializeField]
        private bool _maxDeviceFps;


        protected void Awake()
        {
            int fps = -1;
            if (_max)
            {
                fps = 300;
            }
            else if (_maxDeviceFps)
            {
                fps = Screen.currentResolution.refreshRate;
            }
            else
            {
                fps = _fps;
            }

            Debug.Log($"<color=yellow>[FPS]</color> Set to: {fps}");
            Application.targetFrameRate = fps;
        }
    }
}
