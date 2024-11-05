using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HeartbeatScanner : MonoBehaviour
{
    [SerializeField]
    private TrailRenderer _trailRenderer;

    [SerializeField]
    private Transform _heartbeatTransform;

    [SerializeField]
    private Transform _heartbeatStartTransform;

    [SerializeField]
    private Transform _heartbeatEndTransform;
    
    private void Awake()
    {
        _trailRenderer = GetComponent<TrailRenderer>();
        _heartbeatTransform.position = _heartbeatStartTransform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        // Vector2 heartbeatPosition = _heartbeatTransform.position;
        //
        // heartbeatPosition.x += Time.deltaTime;
        //
        // if (heartbeatPosition.x >= _heartbeatEndTransform.position.x)
        // {
        //     heartbeatPosition = _heartbeatStartTransform.position;
        // }
        //
        // _heartbeatTransform.position = heartbeatPosition;
    }
    
    
}
