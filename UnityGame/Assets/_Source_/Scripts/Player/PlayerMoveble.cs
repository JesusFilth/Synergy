using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerAnimation))]
public class PlayerMoveble : MonoBehaviour
{
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";

    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _rotationSpeed = 300f;
    [SerializeField] private float _magnitude = 0.05f;

    private CharacterController _controller;
    private PlayerAnimation _animator;
    private Vector3 _moveDirection = Vector3.zero;
    private Transform _transform;

    public bool IsWalk { get; private set; }

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<PlayerAnimation>();
        _transform = transform;
    }

    private void Update()
    {
        SetMoveDirection();

        Move();
        Rotate();
        CheckChangeWalkAnimation();
    }

    private void SetMoveDirection()
    {
        float moveHorizontal = Input.GetAxis(Horizontal);
        float moveVertical = Input.GetAxis(Vertical);

        Vector3 forward = _transform.TransformDirection(Vector3.forward);
        Vector3 right = _transform.TransformDirection(Vector3.right);

        _moveDirection = (forward * moveVertical + right * moveHorizontal).normalized;
    }

    private void Move()
    {
        if (_moveDirection.magnitude < _magnitude)
            return;

        _controller.Move(_moveDirection * _speed * Time.deltaTime);
    }

    private void Rotate()
    {
        if (_moveDirection.magnitude < _magnitude)
            return;

        Quaternion toRotation = Quaternion.LookRotation(_moveDirection, Vector3.up);

        _transform.rotation = Quaternion.RotateTowards(_transform.rotation, toRotation, _rotationSpeed * Time.deltaTime);
    }

    private void CheckChangeWalkAnimation()
    {
        bool isWalk = _moveDirection.magnitude >= _magnitude;

        if (IsWalk == isWalk)
            return;

        IsWalk = isWalk;
        _animator.ToWalk(IsWalk);
    }
}