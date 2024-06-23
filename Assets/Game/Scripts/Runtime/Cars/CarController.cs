#region

using Core.Runtime.Base;
using UnityEngine;

#endregion

namespace Game.Runtime.Cars
{
    public class CarController : BaseBehaviour
    {
        private const string HorizontalAxis = "Horizontal";
        private const string VerticalAxis = "Vertical";

        [SerializeField] private Vector2 _borders = new Vector2(-3f, 3f);
        [SerializeField] private float _targetSpeed = 100f;
        [SerializeField] private Transform _target;
        [SerializeField] private Transform _model;
        [SerializeField] private Transform _planet;

        private Vector2 _movementDirection;

        protected void Update()
        {
            _movementDirection = new Vector2(-Input.GetAxis(HorizontalAxis), Input.GetAxis(VerticalAxis));
        }

        protected void FixedUpdate()
        {
            Vector3 targetPosition = _target.localPosition;
            Vector3 moveTo = new Vector3(-1f,
                Mathf.Clamp(targetPosition.y + _movementDirection.y * (_targetSpeed * Time.deltaTime), _borders.x, _borders.y),
                Mathf.Clamp(targetPosition.z + _movementDirection.x * (_targetSpeed * Time.deltaTime), _borders.x, _borders.y));

            _target.localPosition = moveTo;

            Vector3 diff = _target.position - _model.position;
            diff.Normalize();

            float rotationX = Mathf.Atan2(diff.z, diff.y) * Mathf.Rad2Deg;

            Vector3 rotation = _planet.localEulerAngles;
            _planet.localRotation = Quaternion.Euler(0f, rotation.y - _movementDirection.x, rotation.z + _movementDirection.y);
            _model.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        }

    }
}
