using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SpriteRenderer),typeof(Animator))]
public class NPC : MonoBehaviour, IInteractable
{
    [SerializeField]
    private GameObject _tooltipGO;

    [SerializeField]
    private Image _npcIconImage;

    [SerializeField]
    private Image _npcNameTextImage;
    
    private List<NPCScriptEntity> _npcScripts = new();
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _npcIconImage.fillAmount = 0;
        _npcNameTextImage.color = new Color32(255, 255, 255, 0);
    }

    public void Interact()
    {
        BarOutsideDialougeManager.Inst.StartDialogue(transform.position, _npcScripts);
    }

    public void ShowInteractableUI()
    {
        _npcIconImage.DOKill();
        _npcNameTextImage.DOKill();
        
        _tooltipGO.SetActive(true);

        _npcIconImage.DOFillAmount(1f, 0.3f);
        _npcNameTextImage.DOFade(1f, 0.3f);
    }

    public void HideInteractableUI()
    {
        _npcIconImage.DOKill();
        _npcNameTextImage.DOKill();

        _npcNameTextImage.DOFade(0f, 0.3f);
        _npcIconImage.DOFillAmount(0f, 0.3f)
            .OnKill(() =>
            {
                _tooltipGO.SetActive(false);
            });
    }

    public void SetNPC(NPCData npcData, List<NPCScriptEntity> npcScriptEntities)
    {
        _animator.runtimeAnimatorController = npcData.AnimatorController;
        _npcScripts = npcScriptEntities;
        
        BoxCollider2D boxCollider2D = gameObject.AddComponent<BoxCollider2D>();
        boxCollider2D.isTrigger = true;
        _npcNameTextImage.sprite = npcData.NPCNameSprite;
        _npcNameTextImage.SetNativeSize();
    }
}
