using System;
using UnityEngine;

public class FollowerMover : MonoBehaviour
{
    [SerializeField] private Transform _target; // Цель (игрок)
    [SerializeField] private float _speed = 5f; // Скорость движения
    [SerializeField] private LayerMask _groundLayerMask; // Слой земли
    [SerializeField] private float _rayLength = 1.5f; // Длина луча для проверки земли
    [SerializeField] private float _jumpForce = 5f; // Сила прыжка
    [SerializeField]private float _gravityFactor;

    private Rigidbody _rigidbody;
    private bool _isGrounded; // Флаг нахождения на земле
    private Vector3 _verticalVelocity;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (_target == null)
            throw new NullReferenceException();

        // Движение к цели
        Vector3 direction = (_target.position - transform.position).normalized;
        _rigidbody.velocity = new Vector3(direction.x * _speed, _rigidbody.velocity.y, direction.z * _speed);

        // Проверяем, находится ли бот на земле
        _isGrounded = Physics.Raycast(transform.position, Vector3.down, _rayLength, _groundLayerMask);

        // Если бот на земле и цель выше, выполняем прыжок
        if (_isGrounded && _target.position.y > transform.position.y + 0.5f)
        {
            print("bruh");
            Jump();
        }
        
        _verticalVelocity += Physics.gravity * Time.deltaTime * _gravityFactor;

    }

    private void Jump()
    {
        _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }
}