using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MovingPositionPortal : Portal
{
    [SerializeField]
    private Transform _targetTransform;

    [SerializeField]
    private Transform _movingTargetTransform;

    [SerializeField]
    private CinemachineVirtualCamera _originalVirtualCamera;

    [SerializeField]
    private CinemachineVirtualCamera _targetVirtualCamera;

    [SerializeField]
    private Image _fadeImage;

    [SerializeField]
    private Lock _lock;

    [SerializeField]
    private List<string> _cantGoToOrleansScripts;

    public override void Interact()
    {
        if (gameObject.name == "ChannelToOrleansPortal" && _lock.IsInteractFirstTime == false)
        {
            BarOutsideDialougeManager.Inst.StartDialogueByString(_movingTargetTransform.position,
                _cantGoToOrleansScripts);

            return;
        }
        PlayerController.RestrictMovement();
        AudioManager.Inst.PlaySFX("stair_1");
        _fadeImage.DOFade(1f, 0.4f)
            .OnKill(() =>
            {
                _originalVirtualCamera.enabled = false;
                _targetVirtualCamera.enabled = true;
                _movingTargetTransform.position = _targetTransform.position;
                PlayerController.AllowMovement();
                _fadeImage.DOFade(0f, 0.4f);
            });
    }
}
