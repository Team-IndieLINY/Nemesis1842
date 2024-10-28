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
    private GameObject _dayTextGO;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;

    private IInteractable _currentInteractable;

    private Animator _animator;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
        AnimateDayText();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
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
        else if (Input.GetKeyDown(KeyCode.E) && _currentInteractable != null && BarOutsideDialougeManager.Inst.IsProgressed is false)
        {
            _animator.SetBool("IsWalking", false);
            _rigidbody.velocity = new Vector2(0,0);
            _currentInteractable.HideInteractableUI();
            _currentInteractable.Interact();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (BarOutsideDialougeManager.Inst.IsProgressed is false)
        {
            float inputX = Input.GetAxisRaw("Horizontal");

            if (inputX < 0)
            {
                _animator.SetBool("IsWalking", true);
                _spriteRenderer.flipX = true;
            }
            else if(inputX > 0)
            {
                _animator.SetBool("IsWalking", true);
                _spriteRenderer.flipX = false;
            }
            else
            {
                _animator.SetBool("IsWalking", false);
            }

            _rigidbody.velocity = new Vector3(inputX * _movingSpeed, 0, 0);
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

    private void AnimateDayText()
    {
        StartCoroutine(AnimateDayTextCoroutine());
    }

    private IEnumerator AnimateDayTextCoroutine()
    {        
        _dayTextGO.SetActive(true);

        yield return new WaitForSeconds(1.5f);
        
        _dayTextGO.SetActive(false);
    }
}
