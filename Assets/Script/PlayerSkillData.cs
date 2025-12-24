using UnityEngine;


[CreateAssetMenu(fileName = "PlayerSkill", menuName = "Scriptable Objects/PlayerSkill")]
public class PlayerSkillData : ScriptableObject
{
    [Header("Cost")]
    [SerializeField] SkillData _perspectiveCost;
    [SerializeField] SkillData _certainHitCost;
    [SerializeField] SkillData _revealHitGumCost;
    [SerializeField] SkillData _highRateCost;
    [SerializeField] SkillData _noRiskHighReturnCost;
    [SerializeField] SkillData _pointUpCost;
    [SerializeField] SkillData _discountCost;
    [Header("Other")]
    [SerializeField] int _revealGumCount = 3;
    [SerializeField] int _pointDownValue = 10;
    [SerializeField] int _discountValue = 2;
    public int PerspectiveCost => _perspectiveCost.cost;
    public int CertainHitCost => _certainHitCost.cost;
    public int RevealHitGumCost => _revealHitGumCost.cost;
    public int RevealGumCount => _revealGumCount;
    public int HighRateCost => _highRateCost.cost;
    public int NoRiskHighReturnCost => _noRiskHighReturnCost.cost;
    public int PointUpCost => _pointUpCost.cost;
    public int PointDownValue => _pointDownValue;
    public int DiscountCost => _discountCost.cost;
    public int DiscountValue => _discountValue;
}
