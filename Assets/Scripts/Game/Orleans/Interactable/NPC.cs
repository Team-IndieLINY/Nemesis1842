using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class NPC : MonoBehaviour, IInteractable
{
    [SerializeField]
    private GameObject _tooltipGO;
    private List<NPCScriptEntity> _npcScripts = new();
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        PolygonCollider2D polygonCollider2D = gameObject.AddComponent<PolygonCollider2D>();
        polygonCollider2D.isTrigger = true;
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
        _spriteRenderer.sprite = npcData.NPCSprite;
        _npcScripts = npcScriptEntities;
    }
}
