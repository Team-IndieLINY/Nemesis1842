using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class Leaven : MonoBehaviour
{
    private void Start()
    {
        Move();
    }

    private void Move()
    {
        transform.DOShakePosition(0.3f, 0.02f, 20, 40f, false, false)
            .SetLoops(-1, LoopType.Incremental);
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }
}
