using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class TasteLiquid : MonoBehaviour
{
    [Serializable]
    private class TasteLiquidFrame
    {
        [SerializeField]
        private Sprite[] _tasteLiquidStartAnimationFrames;

        public Sprite[] TasteLiquidStartAnimationFrames => _tasteLiquidStartAnimationFrames;
        
        [SerializeField]
        private Sprite[] _tasteLiquidLoopAnimationFrames;

        public Sprite[] TasteLiquidLoopAnimationFrames => _tasteLiquidLoopAnimationFrames;
        
        [SerializeField]
        private Sprite[] _tasteLiquidEndAnimationFrames;

        public Sprite[] TasteLiquidEndAnimationFrames => _tasteLiquidEndAnimationFrames;
    }
    
    [SerializeField]
    private TasteLiquidFrame[] _tasteLiquidFrames;
    
    [SerializeField]
    private CocktailMachineManager _cocktailMachineManager;

    [SerializeField]
    private CocktailMakingManager _cocktailMakingManager;

    [SerializeField]
    private AudioClip _liquidSound;

    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _image.material.SetColor("_BaseColor", new Color(0f, 0f, 0f, 0f));        _image.material.SetColor("_BaseColor", new Color(255, 255, 255, 0));
    }
    
    public void AnimateStartFallingOffLiquid(TasteMachine.TasteType? tasteType)
    {
        _cocktailMachineManager.InActivateCocktailSelectingButtons();
        _cocktailMakingManager.InActivateDoneAndResetButton();
        AudioManager.Instance.PlaySFX(_liquidSound);
        StartCoroutine(StartFallingOffCoroutine(tasteType));
    }

    private IEnumerator StartFallingOffCoroutine(TasteMachine.TasteType? tasteType)
    {
        _image.material.SetColor("_BaseColor", new Color(1f, 1f, 1f, 1f));
        foreach (var sprite in _tasteLiquidFrames[(int)tasteType].TasteLiquidStartAnimationFrames)
        {
            _image.sprite = sprite;
            yield return new WaitForSeconds(0.1f);
        }

        for (int i = 0; i < 10; i++)
        {
            foreach (var sprite in _tasteLiquidFrames[(int)tasteType].TasteLiquidLoopAnimationFrames)
            {
                _image.sprite = sprite;
                yield return new WaitForSeconds(0.05f);
            }
        }

        foreach (var sprite in _tasteLiquidFrames[(int)tasteType].TasteLiquidEndAnimationFrames)
        {
            _image.sprite = sprite;
            yield return new WaitForSeconds(0.1f);
        }

        _image.material.SetColor("_BaseColor", new Color(0f, 0f, 0f, 0f));
        
        _cocktailMachineManager.ActivateCocktailSelectingButtons();
        _cocktailMakingManager.ActivateDoneAndResetButton();
    }
}
