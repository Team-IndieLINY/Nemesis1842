using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class Leaven : MonoBehaviour
{
    private Vector2 _movePosition;
    private float _moveTime;
    private void Start()
    {
        float x = Random.Range(-18, 19) * 0.01f;
        float y = Random.Range(-18, 19) * 0.01f;
        _moveTime = Random.Range(10, 31) * 0.1f;

        _movePosition = new Vector2(transform.position.x + x, transform.position.y + y);
        Move();
    }

    private void Move()
    {
        transform.DOMove(_movePosition, _moveTime).SetLoops(-1, LoopType.Yoyo);
        // transform.DOShakePosition(0.3f, 0.02f, 20, 40f, false, false)
        //     .SetLoops(-1, LoopType.Incremental);
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }
}
