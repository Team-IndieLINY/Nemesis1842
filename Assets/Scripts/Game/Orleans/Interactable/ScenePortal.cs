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
    private Transform _playerTransform;

    public override void Interact()
    {
        if (DayManager.Instance.TimeType == _interactableTimeType)
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
