using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "ScriptableObject/CharacterDatabaseData", fileName = "CharacterDatabaseData", order = Int32.MaxValue)]
public class CharacterDatabaseData : DatabaseData
{
    [SerializeField]
    private string _nameLabel;
    public string NameLabel => _nameLabel;

    [SerializeField]
    private string _relationshipLabel;
    public string RelationshipLabel => _relationshipLabel;

    [SerializeField]
    private string _birthDayLabel;
    public string BirthDayLabel => _birthDayLabel;

    [SerializeField]
    private string _addressLabel;
    public string AddressLabel => _addressLabel;

    [SerializeField]
    private string _jobLabel;
    public string JobLabel => _jobLabel;

    [SerializeField]
    private string _phoneNumberLabel;
    public string PhoneNumberLabel => _phoneNumberLabel;

    [SerializeField]
    private string _SNSIDLabel;
    public string SNSIDLabel => _SNSIDLabel;

    [SerializeField]
    private Sprite _picture;
    public Sprite Picture => _picture;

    // 휘발성 데이터
    [SerializeField]
    private List<string> _volatileData;
    public List<string> VolatileData => _volatileData;

    // 특이 사항
    [SerializeField]
    private List<string> _significant;
    public List<string> Significant => _significant;

    // 행동 대응
    [SerializeField]
    private List<string> _behavioralResponse;
    public List<string> BehavioralResponse => _behavioralResponse;

    // 인간 관계

    [System.Serializable]
    public struct HumanRelationshipsData
    {
        public string Relationship;
        public CharacterDatabaseData CharacterDatabaseData;
    }

    [SerializeField]
    private HumanRelationshipsData[] _humanRelationships;
    public HumanRelationshipsData[] HumanRelationships => _humanRelationships;

}