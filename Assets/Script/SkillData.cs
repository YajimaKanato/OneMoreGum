using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "Scriptable Objects/SkillData")]
public class SkillData : ScriptableObject
{
    [SerializeField] GumDefault.Lotto _type;
    [SerializeField] int _cost;
    [SerializeField, TextArea] string _info;
    public GumDefault.Lotto Type => _type;
    public int cost => _cost;
    public string Info => _info;
}
