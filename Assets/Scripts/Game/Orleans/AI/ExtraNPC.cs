using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D),typeof(Animator),typeof(SpriteRenderer))]
public class ExtraNPC : MonoBehaviour
{
    [SerializeField]
    private float _movingSpeed;

    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private int _radomAxis = 0;
    private float _changeStateCoolTime;
    private float _currentCoolTime = 0f;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
        _radomAxis = Random.Range(-1, 2);
        _changeStateCoolTime = Random.Range(3, 7);
    }

    private void FixedUpdate()
    {
        _currentCoolTime += Time.deltaTime;
        if (_currentCoolTime >= _changeStateCoolTime)
        {
            _radomAxis = Random.Range(-1, 2);
            _changeStateCoolTime = Random.Range(3, 7);
            _currentCoolTime = 0f;
        }
        
        if (_radomAxis < 0)
        {
            _animator.SetBool("IsWalking", true);
            _animator.SetBool("IsIdle", false);
            _spriteRenderer.flipX = true;
        }
        else if(_radomAxis > 0)
        {
            _animator.SetBool("IsWalking", true);
            _animator.SetBool("IsIdle", false);
            _spriteRenderer.flipX = false;
        }
        else
        {
            _animator.SetBool("IsWalking", false);
            _animator.SetBool("IsIdle", true);
        }

        _rigidbody2D.velocity = new Vector3(_radomAxis * _movingSpeed, 0, 0);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        _radomAxis = 0;
    }
}
