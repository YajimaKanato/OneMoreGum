using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDefault", menuName = "Scriptable Objects/PlayerDefault")]
public class PlayerDefault : ScriptableObject
{
    [SerializeField] int _defaultMoney = 100;
    [SerializeField] int _defaultHitCount = 0;
    [SerializeField] int _defaultTotalHitCount = 0;
    [SerializeField] int _defaultMissCount = 0;
    [SerializeField] int _perspectiveCost = 1;
    [SerializeField] int _certainHitCost = 5;
    [SerializeField] int _revealHitGumCost = 10;
    [SerializeField] int _revealGumCount = 3;
    public int DefaultMoney => _defaultMoney;
    public int DefaultHitCount => _defaultHitCount;
    public int DefaultTotalHitCount => _defaultTotalHitCount;
    public int DefaultMissCount => _defaultMissCount;
    public int PerspectiveCost => _perspectiveCost;
    public int CertainHitCost => _certainHitCost;
    public int RevealHitGumCost => _revealHitGumCost;
    public int RevealGumCount => _revealGumCount;
}

public class PlayerSkill
{
    PlayerDefault _player;
    int _currentMoney;
    int _hitCount;
    int _totalHitCount;
    int _missCount;
    int _revealGumCount;
    bool _isPurchasable;
    bool _isCertainHit;
    public int CurrentMoney => _currentMoney;
    public int HitCount => _hitCount;
    public int TotalHitCount => _totalHitCount;
    public int MissCount => _missCount;
    public int RevealGumCount => _revealGumCount;
    public bool IsPurchasable => _isPurchasable;
    public bool IsCertainHit => _isCertainHit;

    public PlayerSkill(PlayerDefault data)
    {
        _player = data;
        _currentMoney = _player.DefaultMoney;
        _hitCount = _player.DefaultHitCount;
        _totalHitCount = _player.DefaultTotalHitCount;
        _missCount = _player.DefaultMissCount;
        _revealGumCount = _player.RevealGumCount;
        _isPurchasable = true;
    }

    public bool CertainHitModeActivation()
    {
        if (_isCertainHit) return false;
        var cost = _player.CertainHitCost;
        if (_missCount < cost) return false;
        _isCertainHit = true;
        _missCount -= _player.CertainHitCost;
        return true;
    }

    public void CertainHitModeDeactivation()
    {
        _isCertainHit = false;
    }

    public bool RevealHitGum()
    {
        var cost = _player.RevealHitGumCost;
        if (_missCount < cost) return false;
        _missCount -= cost;
        return true;
    }

    public bool PurchaseGum(int money)
    {
        if (!_isPurchasable) return false;
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

    public void GetHitGum()
    {
        _totalHitCount++;
        _hitCount++;
        _isPurchasable = true;
    }

    public void GetMissGum()
    {
        _missCount++;
    }

    public bool Perspective()
    {
        var cost = _player.PerspectiveCost;
        if (_missCount < cost) return false;
        _missCount -= cost;
        return true;
    }
}