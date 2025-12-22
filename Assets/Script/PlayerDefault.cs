using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDefault", menuName = "Scriptable Objects/PlayerDefault")]
public class PlayerDefault : ScriptableObject
{
    [SerializeField] int _defaultMoney = 100;
    [SerializeField] int _defaultHitCount = 0;
    [SerializeField] int _defaultMissCount = 0;
    [SerializeField] int _perspectiveCost = 5;
    public int DefaultMoney => _defaultMoney;
    public int DefaultHitCount => _defaultHitCount;
    public int DefaultMissCount => _defaultMissCount;
    public int PerspectiveCost => _perspectiveCost;
}

public class PlayerSkill
{
    PlayerDefault _player;
    int _currentMoney;
    int _hitCount;
    int _missCount;
    bool _isPurchasable;
    public int CurrentMoney => _currentMoney;
    public int HitCount => _hitCount;
    public int MissCount => _missCount;
    public bool IsPurchasable => _isPurchasable;

    public PlayerSkill(PlayerDefault data)
    {
        _player = data;
        _currentMoney = _player.DefaultMoney;
        _hitCount = _player.DefaultHitCount;
        _missCount = _player.DefaultMissCount;
        _isPurchasable = true;
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
        _hitCount++;
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