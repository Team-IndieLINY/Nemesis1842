using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    [SerializeField]
    private Sprite[] _npcTypeSprites;
    
    private List<NPCScriptEntity> _npcFirstInteractScripts = new();
    private List<NPCScriptEntity> _npcNotFirstInteractScripts = new();
    
    private Animator _animator;
    private NPCData _npcData;
    
    public bool IsInteracted { get; set; }

    private void Awake()
    {
        IsInteracted = false;
        _animator = GetComponent<Animator>();
        _npcIconImage.fillAmount = 0;
        _npcNameTextImage.color = new Color32(255, 255, 255, 0);
    }

    public void Interact()
    {
        if (IsInteracted is false)
        {
            BarOutsideDialougeManager.Inst.StartDialogueByNPCDialougeEntity(_npcData, _npcFirstInteractScripts, this);
        }
        else
        {
            BarOutsideDialougeManager.Inst.StartDialogueByNPCDialougeEntity(_npcData, _npcNotFirstInteractScripts, this);
        }
    }

    public void ShowInteractableUI()
    {
        _npcIconImage.DOKill();
        _npcNameTextImage.DOKill();
        
        _tooltipGO.SetActive(true);

        _npcIconImage.DOFillAmount(1f, 0.3f);

        if (_npcData.NPCType == NPCData.ENPCType.Object)
        {
            return;
        }
        _npcNameTextImage.DOFade(1f, 0.3f);
    }

    public void HideInteractableUI()
    {
        _npcIconImage.DOKill();
        _npcNameTextImage.DOKill();

        if (_npcData.NPCType == NPCData.ENPCType.Human)
        {
            _npcNameTextImage.DOFade(0f, 0.3f);
        }

        _npcIconImage.DOFillAmount(0f, 0.3f)
            .OnKill(() =>
            {
                _tooltipGO.SetActive(false);
            });
    }

    public void SetNPC(NPCData npcData, List<NPCScriptEntity> npcScriptEntities)
    {
        _npcData = npcData;
        
        _animator.runtimeAnimatorController = _npcData.AnimatorController;

        _npcFirstInteractScripts = npcScriptEntities.Where(x => x.interact_state == 0).ToList();
        _npcNotFirstInteractScripts = npcScriptEntities.Where(x => x.interact_state == 1).ToList();
        
        _npcNameTextImage.sprite = _npcData.NPCNameSprite;
        _npcNameTextImage.SetNativeSize();

        _npcIconImage.sprite = _npcTypeSprites[(int)npcData.NPCType];
    }
}
