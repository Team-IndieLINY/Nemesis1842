using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterDatabase : Database
{
    [SerializeField]
    private CharacterDatabaseData _currentChracterDatabaseData;

    [SerializeField]
    private TextMeshProUGUI _nameLabel;

    [SerializeField]
    private TextMeshProUGUI _relationshipLabel;

    [SerializeField]
    private TextMeshProUGUI _birthDayLabel;

    [SerializeField]
    private TextMeshProUGUI _addressLabel;

    [SerializeField]
    private TextMeshProUGUI _jobLabel;

    [SerializeField]
    private TextMeshProUGUI _phoneNumberLabel;

    [SerializeField]
    private TextMeshProUGUI _SNSIDLabel;

    [SerializeField]
    private Image _pictureImage;

    public enum DataType
    {
        VolatileData = 0,
        Significant,
        BehavioralResponse,
        HumanRelationships,

    }

    [SerializeField]
    private DataType _currentDataType;

    private static readonly string[] _dataTypeStr = 
    { 
        "휘발성 데이터", 
        "특이 사항", 
        "행동 대응", 
        "인간 관계" 
    };

    [SerializeField]
    private TextMeshProUGUI _titleLabel;

    [SerializeField]
    private GameObject[] _dataLabels;

    private TextMeshProUGUI[] _dataLabelsTexts;

    // 인간 관계
    [SerializeField]
    private RelationshipButtonData[] relationshipButtonDatas;

    [SerializeField]
    private GameObject _dataButtonGroup;

    [SerializeField]
    private GameObject _relationshipButtonGroup;

    public void Initialize()
    {
        _dataLabelsTexts = new TextMeshProUGUI[5];

        for (int i = 0; i < _dataLabels.Length; i++)
        {
            _dataLabelsTexts[i] = _dataLabels[i].GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    public override void OpenDatabase()
    {
        this._preDatabase = _databaseManager.GetCurrentDatabase();

        base.OpenDatabase();
    }

    public void SetDatabase(DatabaseData databaseData)
    {
        CharacterDatabaseData characterDatabaseData = databaseData as CharacterDatabaseData;

        if (characterDatabaseData == null)
        {
            Debug.LogError("characterDatabaseData is null");
            return;
        }

        _currentChracterDatabaseData = characterDatabaseData;

        _nameLabel.text = characterDatabaseData.NameLabel;
        _relationshipLabel.text = characterDatabaseData.RelationshipLabel;
        _birthDayLabel.text = characterDatabaseData.BirthDayLabel;
        _addressLabel.text = characterDatabaseData.AddressLabel;
        _addressLabel.text = characterDatabaseData.AddressLabel;
        _jobLabel.text = characterDatabaseData.JobLabel;
        _phoneNumberLabel.text = characterDatabaseData.PhoneNumberLabel;
        _SNSIDLabel.text = characterDatabaseData.SNSIDLabel;
        _pictureImage.sprite = characterDatabaseData.Picture;

        _currentDataType = DataType.VolatileData;
        _titleLabel.text = _dataTypeStr[(int)DataType.VolatileData];

        ResetCurrentDataType();
        SetStringDatas(characterDatabaseData.VolatileData);
        SetActiveButtonGroup();
    }

    public void SetPage(int page)
    {
        DataType dataType = (DataType)page;
        if (_currentDataType == dataType)
            return;

        _currentDataType = dataType;
        _titleLabel.text = _dataTypeStr[(int)_currentDataType];

        SetActiveButtonGroup();

        switch (_currentDataType)
        {
            case DataType.VolatileData:
                SetStringDatas(_currentChracterDatabaseData.VolatileData);
                break;
            case DataType.Significant:
                SetStringDatas(_currentChracterDatabaseData.Significant);
                break;
            case DataType.BehavioralResponse:
                SetStringDatas(_currentChracterDatabaseData.BehavioralResponse);
                break;
            case DataType.HumanRelationships:

                int dataCount = _currentChracterDatabaseData.HumanRelationships.Length;
                for (int i = 0; i < dataCount; i++)
                {
                    relationshipButtonDatas[i].RelationshipLabel.text = 
                        _currentChracterDatabaseData.HumanRelationships[i].Relationship;

                    relationshipButtonDatas[i].NameLabel.text =
                        _currentChracterDatabaseData.HumanRelationships[i].CharacterDatabaseData.NameLabel;

                    relationshipButtonDatas[i].PictureImage.sprite =
                        _currentChracterDatabaseData.HumanRelationships[i].CharacterDatabaseData.Picture;

                    relationshipButtonDatas[i].gameObject.SetActive(true);
                }
                for (int i = dataCount; i < relationshipButtonDatas.Length; i++)
                {
                    relationshipButtonDatas[i].gameObject.SetActive(false);
                }

                break;
        }
    }

    private void SetStringDatas(List<string> strDatas)
    {
        int dataCount = strDatas.Count;
        for (int i = 0; i < dataCount; i++)
        {
            _dataLabelsTexts[i].text = strDatas[i];
            _dataLabels[i].SetActive(true);
        }
        for (int i = dataCount; i < _dataLabels.Length; i++)
        {
            _dataLabels[i].SetActive(false);
        }
    }

    private void SetActiveButtonGroup()
    {
        switch (_currentDataType)
        {
            case DataType.VolatileData:
            case DataType.Significant:
            case DataType.BehavioralResponse:
                _dataButtonGroup.SetActive(true);
                _relationshipButtonGroup.SetActive(false);
                break;
            case DataType.HumanRelationships:
                _dataButtonGroup.SetActive(false);
                _relationshipButtonGroup.SetActive(true);
                break;
        }
    }

    public void SetRelationshipDatabase(int page)
    {
        SetDatabase(_currentChracterDatabaseData.HumanRelationships[page].CharacterDatabaseData);
    }

    public void ResetCurrentDataType()
    {
        _currentDataType = DataType.VolatileData;
        _titleLabel.text = _dataTypeStr[(int)DataType.VolatileData];
    }
}
