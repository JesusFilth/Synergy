using UnityEngine;

public class ItemDamageRotate : MonoBehaviour
{
    [SerializeField] private float _speed = 40f;
    [SerializeField] private float _distance = 1f;
    [SerializeField] private Transform _target;

    private float _currentAngle = 0f;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        _currentAngle += _speed * Time.deltaTime;

        float radians = _currentAngle * Mathf.Deg2Rad;
        Vector3 newPosition = new Vector3(
            _target.position.x + Mathf.Cos(radians) * _distance,
            _transform.position.y,
            _target.position.z + Mathf.Sin(radians) * _distance
        );

        _transform.position = newPosition;
    }
}