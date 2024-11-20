using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ScenePortal : Portal
{
    [SerializeField]
    private string _sceneName;
    
    [SerializeField]
    private NPCData.ETimeType _interactableTimeType;
    
    [SerializeField]
    private Image _fadeImage;

    [SerializeField]
    private List<string> _inactivatedScript;
    
    [SerializeField]
    private List<string> _youNeedToUnLockScript;

    [SerializeField]
    private Transform _playerTransform;

    [SerializeField]
    private DayEndingUI _dayEndingUI;

    public override void Interact()
    {
        if (gameObject.name == "HousePortal" && DayManager.Instance.Day == 3 &&
                 DayManager.Instance.TimeType == NPCData.ETimeType.Dawn)
        {
            if (_youNeedToUnLockScript.Count == 0)
            {
                return;
            }
            
            BarOutsideDialougeManager.Inst.StartDialogueByString(_playerTransform.position, _youNeedToUnLockScript);
        }
        else if (gameObject.name == "BarPortal" && DayManager.Instance.Day == 1 &&
                         DayManager.Instance.TimeType == NPCData.ETimeType.Evening)
        {
            StartCoroutine(CutSceneManager.Inst.StartCutScene());
        }
        else if (DayManager.Instance.TimeType == _interactableTimeType)
        {
            PlayerController.RestrictMovement();
            if (DayManager.Instance.TimeType == NPCData.ETimeType.Evening)
            {
                AudioManager.Inst.FadeOutMusic("evening");
            }
            else if (DayManager.Instance.TimeType == NPCData.ETimeType.Dawn)
            {
                AudioManager.Inst.FadeOutMusic("dawn");
            }
            _fadeImage.DOFade(1f, 1f)
                .OnKill(() =>
                {
                    if (gameObject.name == "HousePortal")
                    {
                        _dayEndingUI.OpenDayEndingUI();
                        return;
                    }
                    
                    PlayerController.AllowMovement();
                    LoadingScreen.Instance.LoadScene(_sceneName);
                });
        }
        else
        {
            if (_inactivatedScript.Count == 0)
            {
                return;
            }
            
            BarOutsideDialougeManager.Inst.StartDialogueByString(_playerTransform.position, _inactivatedScript);
        }
    }
}
