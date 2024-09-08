using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RelationshipButtonData : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _relationshipLabel;
    public TextMeshProUGUI RelationshipLabel => _relationshipLabel;

    [SerializeField]
    private TextMeshProUGUI _nameLabel;
    public TextMeshProUGUI NameLabel => _nameLabel;

    [SerializeField]
    private Image _pictureImage;
    public Image PictureImage => _pictureImage;

}
