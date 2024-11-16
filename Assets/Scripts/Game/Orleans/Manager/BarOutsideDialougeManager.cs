using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BarOutsideDialougeManager : MonoBehaviour
{
    public static BarOutsideDialougeManager Inst { get; private set; }
    
    [SerializeField]
    private GameObject _chatBalloonGO;
    
    [SerializeField]
    private TextMeshProUGUI _characterNameText;

    [SerializeField]
    private TextMeshProUGUI _scriptText;

    [SerializeField, Range(0.01f, 0.3f)]
    private float _typeSpeedForSecond;

    [SerializeField]
    private AudioClip _typingAudioClip;

    [SerializeField]
    private Inventory _inventory;

    [SerializeField]
    private PlayerOutside _playerOutside;
    
    private Queue<string> _scriptsQueue = new Queue<string>();

    [SerializeField]
    private ItemAquirePopup _itemAquirePopup;

    private bool _isProgressed = false;
    public bool IsProgressed => _isProgressed;
    
    private bool _isTyped = false;
    public bool IsTyped => _isTyped;

    private string _currentScript;

    private Coroutine _typeScriptsCoroutine;

    private NPCData _currentNPCData;
    private NPC _currentNPC;

    private void Awake()
    {
        Inst = this;
        _chatBalloonGO.SetActive(false);
    }
    
    public void StartDialogueByNPCDialougeEntity(NPCData npcData, List<NPCScriptEntity> barDialogueEntities,NPC npc)
    {
        PlayerController.RestrictMovement();
        _currentNPC = npc;
        _chatBalloonGO.transform.position = new Vector3(npcData.SpawnPosition.x, npcData.SpawnPosition.y + 1.7f, 0);

        _currentNPCData = npcData;
        
        _isProgressed = true;
        
        _chatBalloonGO.SetActive(true);
        
        _scriptsQueue.Clear();
        
        foreach (var barDialogueEntity in barDialogueEntities)
        {
            _scriptsQueue.Enqueue(barDialogueEntity.script);
        }

        DisplayNextScript();
    }
    
    public void StartDialogueByString(Vector3 characterPosition, List<string> barDialogueEntities)
    {
        _currentNPCData = null;
        
        PlayerController.RestrictMovement();
        
        _chatBalloonGO.transform.position = new Vector3(characterPosition.x, characterPosition.y + 1.7f, 0);
        
        _isProgressed = true;
        
        _chatBalloonGO.SetActive(true);
        
        _scriptsQueue.Clear();
        
        foreach (var barDialogueEntity in barDialogueEntities)
        {
            _scriptsQueue.Enqueue(barDialogueEntity);
        }

        DisplayNextScript();
    }

    public void DisplayNextScript()
    {
        if (_scriptsQueue.Count == 0)
        {
            EndDialogue();
            return;
        }
        
        _currentScript = _scriptsQueue.Dequeue();
        
        _typeScriptsCoroutine = StartCoroutine(TypeScripts(_currentScript));
    }
    
    private IEnumerator TypeScripts(string script)
    {
        _isTyped = true;
        _scriptText.text = "";

        foreach (var letter in script.ToCharArray())
        {
            AudioManager.Inst.PlaySFX("type");
            _scriptText.text += letter;
            yield return new WaitForSeconds(_typeSpeedForSecond);
        }

        _isTyped = false;
    }

    public void SkipTypeScripts()
    {
        StopCoroutine(_typeScriptsCoroutine);

        _scriptText.text = _currentScript;

        _isTyped = false;
    }
    
    public void EndDialogue()
    {
        _isProgressed = false;
        _chatBalloonGO.SetActive(false);

        Reward();

        if (_currentNPC != null)
        {
            _currentNPC.IsInteracted = true;
        }
        
        PlayerController.AllowMovement();
    }

    private void Reward()
    {
        if (_currentNPCData == null)
        {
            return;
        }

        if (_currentNPC.IsInteracted is true)
        {
            return;
        }
        
        if (_currentNPCData.RewardItemData != null)
        {
            InventoryManager.Instance()
                .AddItem(_currentNPCData.RewardItemData.ItemType, _currentNPCData.RewardItemAmount);

            _itemAquirePopup.SetAquireItem(_currentNPCData.RewardItemData);
            
            PopUpUIManager.Inst.OpenUI(_itemAquirePopup);
        }
        else if (_currentNPCData.RewardStealableItemData != null)
        {
            if (_currentNPCData.RewardStealableItemData is InformationItemData informationItemData)
            {
                _inventory.AddItem(informationItemData);
            }
            else if (_currentNPCData.RewardStealableItemData is MoneyItemData moneyItemData)
            {
                _itemAquirePopup.SetEarnMoney(moneyItemData);
                _playerOutside.EarnMoney(moneyItemData.Money);
                
                PopUpUIManager.Inst.OpenUI(_itemAquirePopup);
            }
        }
    }
}
