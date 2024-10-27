using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController Inst { get; private set; }
    
    [SerializeField]
    private float _movingSpeed;

    [SerializeField]
    private GameObject _dayTextGO;

    private Rigidbody2D _rigidbody;
    
    public bool CanEntry { get; private set; }

    private void Awake()
    {
        Inst = this;
        _rigidbody = GetComponent<Rigidbody2D>();
        AnimateDayText();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float inputX = Input.GetAxisRaw("Horizontal");

        _rigidbody.velocity = new Vector3(inputX * _movingSpeed, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Portal portal))
        {
            portal.ActivateKeyToolTip();
            CanEntry = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Portal portal))
        {
            portal.InActivateKeyToolTip();
            CanEntry = false;
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
