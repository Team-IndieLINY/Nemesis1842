using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class HeartbeatTrailController : MonoBehaviour
{
    private TrailRenderer _heartbeatTrailRenderer;

    private void Awake()
    {
        _heartbeatTrailRenderer = GetComponent<TrailRenderer>();
    }

    public void CleanUpTrail()
    {
        _heartbeatTrailRenderer.material.color = new Color32(255, 255, 255, 255);
        _heartbeatTrailRenderer.Clear();
    }

    public void TransparentHeartbeat()
    {
        _heartbeatTrailRenderer.material.color = new Color32(255, 255, 255, 0);
    }
}
