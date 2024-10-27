using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Belonging : MonoBehaviour
{
    [SerializeField]
    private StealableItem[] _stealableItems;

    public void InActivateStealableItems()
    {
        foreach (var stealableItem in _stealableItems)
        {
            if (stealableItem == null)
            {
                return;
            }
            stealableItem.InActivateStealableItem();
        }
    }

    public void ActivateStealableItems()
    {
        foreach (var stealableItem in _stealableItems)
        {
            if (stealableItem == null)
            {
                return;
            }
            
            stealableItem.ActivateStealableItem();
        }
    }
}
