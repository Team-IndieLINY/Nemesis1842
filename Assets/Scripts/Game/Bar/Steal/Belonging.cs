using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Belonging : MonoBehaviour,IPointerClickHandler
{
    [SerializeField]
    private StealableItem[] _stealableItems;
    
    [SerializeField]
    private Sprite _openBelongingSprite;

    [SerializeField]
    private GameObject _frontGO;

    [SerializeField]
    private GameObject _innerGO;

    private Image _frontImage;
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _frontImage = _frontGO.GetComponent<Image>();
    }

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

    public void OnPointerClick(PointerEventData eventData)
    {
        AudioManager.Inst.PlaySFX("steal_item_1");
        _image.raycastTarget = false;
        
        _image.sprite = _openBelongingSprite;
        _image.SetNativeSize();
        _frontGO.SetActive(true);
        _innerGO.SetActive(true);

        _frontImage.DOColor(new Color32(255, 255, 255, 0), 0.5f);
    }
}
