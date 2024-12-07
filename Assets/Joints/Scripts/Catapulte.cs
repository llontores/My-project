using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
public class Catapulte : MonoBehaviour
{
    [SerializeField] private float _throwForce = 100f;
    [SerializeField] private Missile _missilePrefab;
    [SerializeField] private Transform _missileSpawnPoint;
    [SerializeField] private SpringJoint _springJoint;
    [SerializeField] private float _backThrowForce = 20f;
    [SerializeField] private float _startSpringForce = 30f;
    
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _rigidbody.AddForce(Vector3.up * _throwForce, ForceMode.Impulse);
            _springJoint.spring = 0;
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            Instantiate(_missilePrefab, _missileSpawnPoint.position, Quaternion.identity);
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            _springJoint.spring = _startSpringForce;
            _rigidbody.AddForce(Vector3.forward * _throwForce, ForceMode.Impulse);

        }
    }
}