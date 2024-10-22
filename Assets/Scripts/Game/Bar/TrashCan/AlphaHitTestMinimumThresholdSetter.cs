using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlphaHitTestMinimumThresholdSetter : MonoBehaviour
{
    [SerializeField]
    private List<Image> _images;

    private void Start()
    {
        foreach (var image in _images)
        {
            image.alphaHitTestMinimumThreshold = 1f;
        }
    }
}
