using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer),typeof(Animator))]
public class NPC : MonoBehaviour, IInteractable
{
    [SerializeField]
    private GameObject _tooltipGO;
    private List<NPCScriptEntity> _npcScripts = new();
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Interact()
    {
        BarOutsideDialougeManager.Inst.StartDialogue(transform.position, _npcScripts);
    }

    public void ShowInteractableUI()
    {
        _tooltipGO.SetActive(true);
    }

    public void HideInteractableUI()
    {
        _tooltipGO.SetActive(false);
    }

    public void SetNPC(NPCData npcData, List<NPCScriptEntity> npcScriptEntities)
    {
        _animator.runtimeAnimatorController = npcData.AnimatorController;
        _npcScripts = npcScriptEntities;
        
        BoxCollider2D boxCollider2D = gameObject.AddComponent<BoxCollider2D>();
        boxCollider2D.isTrigger = true;
    }
}
