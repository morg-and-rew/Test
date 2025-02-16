using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private float _minHeightOffset = 3f;
    private float _smoothSpeed = 0.125f;
    private float _rotationSpeed = 5f;
    private Vector3 _cameraOffset = new Vector3(0, 5, -10);
    private float _distanceToTarget;

    private void Start()
    {
        // »нициализируем рассто€ние до цели
        _distanceToTarget = _cameraOffset.magnitude;
    }

    private void LateUpdate()
    {
        if (_target == null)
            return;

        Vector3 desiredPosition = _target.position + _cameraOffset;

        float minHeight = _target.position.y + _minHeightOffset;

        if (desiredPosition.y < minHeight)
            desiredPosition.y = minHeight;

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);
        transform.position = smoothedPosition;

        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X") * _rotationSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * _rotationSpeed;

            // ¬ращаем камеру вокруг цели
            transform.RotateAround(_target.position, Vector3.up, mouseX);
            transform.RotateAround(_target.position, transform.right, -mouseY);

            // ќбновл€ем позицию камеры, сохран€€ фиксированное рассто€ние до цели
            _cameraOffset = transform.position - _target.position;
            _cameraOffset = _cameraOffset.normalized * _distanceToTarget;
            transform.position = _target.position + _cameraOffset;
        }

        transform.LookAt(_target);
    }
}
