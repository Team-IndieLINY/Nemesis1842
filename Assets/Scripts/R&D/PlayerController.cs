using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator),typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _movingSpeed;

    [SerializeField]
    private Inventory _inventory;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;

    private IInteractable _currentInteractable;

    private Animator _animator;

    private static int restrictingMovementCount = 0;

    private float _walkTimeSound = 0.5f;

    private void Awake()
    {
        restrictingMovementCount = 0;
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.E)) && BarOutsideDialougeManager.Inst.IsProgressed is true)
        {
            if (BarOutsideDialougeManager.Inst.IsTyped is true)
            {
                BarOutsideDialougeManager.Inst.SkipTypeScripts();
            }
            else
            {
                BarOutsideDialougeManager.Inst.DisplayNextScript();
            }
        }
        else if (Input.GetKeyDown(KeyCode.E) && _currentInteractable != null && BarOutsideDialougeManager.Inst.IsProgressed is false 
                 && restrictingMovementCount == 0)
        {
            _currentInteractable.HideInteractableUI();
            _currentInteractable.Interact();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (restrictingMovementCount == 0)
        {
            float inputX = Input.GetAxisRaw("Horizontal");

            if (inputX < 0)
            {
                _animator.SetBool("IsWalking", true);
                _spriteRenderer.flipX = true;
                
                _walkTimeSound += Time.deltaTime;
                
                if (_walkTimeSound > 0.5f)
                {
                    AudioManager.Inst.PlaySFX("walk_1");
                    _walkTimeSound = 0f;
                }
            }
            else if(inputX > 0)
            {
                _animator.SetBool("IsWalking", true);
                _spriteRenderer.flipX = false;
                
                _walkTimeSound += Time.deltaTime;

                if (_walkTimeSound > 0.5f)
                {
                    AudioManager.Inst.PlaySFX("walk_1");
                    _walkTimeSound = 0f;
                }
            }
            else
            {
                _animator.SetBool("IsWalking", false);
                _walkTimeSound = 0.5f;
            }

            _rigidbody.velocity = new Vector3(inputX * _movingSpeed, 0, 0);
        }
        else
        {
            _animator.SetBool("IsWalking", false);
            _rigidbody.velocity = new Vector2(0,0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IInteractable interactable))
        {
            _currentInteractable = interactable;

            interactable.ShowInteractableUI();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out IInteractable interactable))
        {
            _currentInteractable = null;
            interactable.HideInteractableUI();
        }
    }

    public static void RestrictMovement()
    {
        restrictingMovementCount++;
    }

    public static void AllowMovement()
    {
        restrictingMovementCount--;
    }
}
