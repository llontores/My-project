using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed = 7f;
    [SerializeField] private float _strafeSpeed = 7f;
    [SerializeField] private float _jumpSpeed = 7f;
    [SerializeField] private float _gravityFactor = 2f;
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private float _horizontalTurnSensitivity = 10f;
    [SerializeField] private float _verticalTurnSensitivity = 10f;
    [SerializeField] private float _verticalMinAngle = -89f;
    [SerializeField] private float _verticalMaxAngle = 89f;

    private Transform _transform;
    private CharacterController _characterController;
    private PlayerInput _playerInput;
    private float _cameraAngle = 0;
    private Vector3 _verticalVelocity;

    private void Awake()
    {
        _transform = transform;
        _characterController = GetComponent<CharacterController>();
        _playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        Vector3 forward = Vector3.ProjectOnPlane(_cameraTransform.forward, Vector3.up).normalized;
        Vector3 right = Vector3.ProjectOnPlane(_cameraTransform.right, Vector3.up).normalized;

        _cameraAngle -= _playerInput.MouseInput.y * _verticalTurnSensitivity;
        _cameraAngle = Mathf.Clamp(_cameraAngle, _verticalMinAngle, _verticalMaxAngle);
        _cameraTransform.localEulerAngles = Vector3.right * _cameraAngle;

        _transform.Rotate(Vector3.up * _horizontalTurnSensitivity * _playerInput.MouseInput.x);

        if (_characterController != null)
        {
            Vector3 playerSpeed = forward * _playerInput.MoveInput.y * _speed + right * _playerInput.MoveInput.x * _strafeSpeed;

            if (_characterController.isGrounded)
            {
                if (_playerInput.JumpInput)
                {
                    _verticalVelocity = Vector3.up * _jumpSpeed;
                }
                else
                {
                    _verticalVelocity = Vector3.down;
                }

                _characterController.Move((playerSpeed + _verticalVelocity) * Time.deltaTime);
            }
            else
            {
                Vector3 horizontalVelocity = _characterController.velocity;
                horizontalVelocity.y = 0;
                _verticalVelocity += Physics.gravity * Time.deltaTime * _gravityFactor;
                _characterController.Move((horizontalVelocity + _verticalVelocity) * Time.deltaTime);
            }
        }
    }
}
