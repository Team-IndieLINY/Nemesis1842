using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Car : MonoBehaviour
{
    [SerializeField] private float _movingSpeed;
    [SerializeField] private Transform _startPointTransform;
    [SerializeField] private Transform _endPointTransform;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        transform.position = _startPointTransform.position;
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _rigidbody.velocity = new Vector3(_movingSpeed, 0, 0);

        if (transform.position.x > _endPointTransform.position.x)
        {
            transform.position = _startPointTransform.position;
        }
    }
}
