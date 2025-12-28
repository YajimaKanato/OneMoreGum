using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDefault", menuName = "Scriptable Objects/PlayerDefault")]
public class PlayerDefault : ScriptableObject
{
    [SerializeField] PlayerSkillData _skill;
    [SerializeField] int _defaultMoney = 100;
    [SerializeField] int _defaultHitCount = 0;
    [SerializeField] int _defaultTotalHitCount = 0;
    [SerializeField] int _defaultMissCount = 0;
    public PlayerSkillData Skill => _skill;
    public int DefaultMoney => _defaultMoney;
    public int DefaultHitCount => _defaultHitCount;
    public int DefaultTotalHitCount => _defaultTotalHitCount;
    public int DefaultMissCount => _defaultMissCount;
}

public class PlayerSkill
{
    PlayerDefault _player;
    PlayerSkillData _skill;
    int _currentMoney;
    int _hitCount;
    int _score;
    int _missCount;
    int _revealGumCount;
    int _rateUpCount;
    int _discountValue;
    bool _isPurchasable;
    bool _isCertainHit;
    bool _isHighRate;
    bool _isNRHRMode;
    bool _isRateUPMode;
    bool _isDiscount;

    public int CurrentMoney => _currentMoney;
    public int HitCount => _hitCount;
    public int Score => _score;
    public int MissCount => _missCount;
    public int RevealGumCount => _revealGumCount;
    public int RateUpCount => _rateUpCount;
    public bool IsPurchasable => _isPurchasable;
    public bool IsCertainHit => _isCertainHit;
    public bool IsHighRate => _isHighRate;

    public PlayerSkill(PlayerDefault data)
    {
        _player = data;
        _skill = data.Skill;
        _currentMoney = _player.DefaultMoney;
        _hitCount = _player.DefaultHitCount;
        _score = _player.DefaultTotalHitCount;
        _missCount = _player.DefaultMissCount;
        _revealGumCount = _skill.RevealGumCount;
        _rateUpCount = _skill.PointDownValue;
        _discountValue = 0;
        _isCertainHit = false;
        _isPurchasable = true;
    }

    public bool CertainHitModeActivation()
    {
        if (_isCertainHit) return false;
        var cost = _skill.CertainHitCost - _discountValue;
        if (cost <= 0) cost = 0;
        if (_missCount < cost) return false;
        _isCertainHit = true;
        _missCount -= cost;
        return true;
    }

    public void CertainHitModeDeactivation()
    {
        _isCertainHit = false;
    }

    public bool RevealHitGum()
    {
        var cost = _skill.RevealHitGumCost - _discountValue;
        if (cost <= 0) cost = 0;
        if (_missCount < cost) return false;
        _missCount -= cost;
        return true;
    }

    public bool HighRateModeActivation()
    {
        if (_isHighRate) return false;
        var cost = _skill.HighRateCost - _discountValue;
        if (cost <= 0) cost = 0;
        if (_missCount < cost) return false;
        _isHighRate = true;
        _missCount -= cost;
        return true;
    }

    public void HighRateModeDeactivation()
    {
        _isHighRate = false;
    }

    public bool PurchaseGum(int money)
    {
        if (!_isPurchasable) return false;
        if (_isNRHRMode) return true;
        if (_currentMoney >= money)
        {
            _currentMoney -= money;
        }
        else if (_hitCount > 0)
        {
            _hitCount--;
        }
        _isPurchasable = _currentMoney >= money || _hitCount > 0;
        return true;
    }

    public void GetHitGum(int score)
    {
        if (_isNRHRMode) _isNRHRMode = false;
        if (_isRateUPMode) _rateUpCount--;
        _score += score;
        _hitCount++;
        _isPurchasable = true;
    }

    public void GetMissGum()
    {
        if (_isRateUPMode) _rateUpCount--;
        _missCount++;
    }

    public bool Perspective()
    {
        var cost = _skill.PerspectiveCost - _discountValue;
        if (cost <= 0) cost = 0;
        if (_missCount < cost) return false;
        _missCount -= cost;
        return true;
    }

    public bool NRHRModeActivation()
    {
        if (_isNRHRMode) return false;
        var cost = _skill.NoRiskHighReturnCost - _discountValue;
        if (cost <= 0) cost = 0;
        if (_missCount < cost) return false;
        _isNRHRMode = true;
        _missCount -= cost;
        return true;
    }

    public void NRHRModeDeactivation()
    {
        _isNRHRMode = false;
    }

    public bool PointUPModeActivation()
    {
        if (_isRateUPMode) return false;
        var cost = _skill.PointUpCost - _discountValue;
        if (cost <= 0) cost = 0;
        if (_missCount < cost) return false;
        _isRateUPMode = true;
        _rateUpCount = _skill.PointDownValue;
        _missCount -= cost;
        return true;
    }

    public void PointUPModeDeactivation()
    {
        _isRateUPMode = false;
    }

    public bool DiscountModeActivation()
    {
        if (_isDiscount) return false;
        var cost = _skill.DiscountCost;
        if (_hitCount < cost) return false;
        _isDiscount = true;
        _hitCount -= cost;
        _discountValue = _skill.DiscountValue;
        return true;
    }

    public void DiscountModeDeactivation()
    {
        _isDiscount = false;
        _discountValue = 0;
    }
}

[System.Serializable]
public class HitGumCounter
{
    public static readonly string FileName = "HitGum";
    public int HitCount;

    public HitGumCounter(int hitCount)
    {
        HitCount = hitCount;
    }
}