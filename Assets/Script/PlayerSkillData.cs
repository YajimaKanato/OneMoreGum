using UnityEngine;


[CreateAssetMenu(fileName = "PlayerSkill", menuName = "Scriptable Objects/PlayerSkill")]
public class PlayerSkillData : ScriptableObject
{
    [Header("Cost")]
    [SerializeField] int _perspectiveCost = 1;
    [SerializeField] int _certainHitCost = 5;
    [SerializeField] int _revealHitGumCost = 10;
    [SerializeField] int _highRateCost = 5;
    [SerializeField] int _noRiskHighReturnCost = 5;
    [Header("Other")]
    [SerializeField] int _revealGumCount = 3;
    public int PerspectiveCost => _perspectiveCost;
    public int CertainHitCost => _certainHitCost;
    public int RevealHitGumCost => _revealHitGumCost;
    public int RevealGumCount => _revealGumCount;
    public int HighRateCost => _highRateCost;
    public int NoRiskHighReturnCost => _noRiskHighReturnCost;
}
