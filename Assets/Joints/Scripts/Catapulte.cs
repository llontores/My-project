using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Catapulte : MonoBehaviour
{
    [SerializeField] private float _throwForce = 100f;
    [SerializeField] private Missile _missilePrefab;
    [SerializeField] private Transform _missileSpawnPoint;
    
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
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            Instantiate(_missilePrefab, _missileSpawnPoint.position, Quaternion.identity);
        }
    }
}