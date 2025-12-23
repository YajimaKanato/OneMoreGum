using UnityEngine;

[CreateAssetMenu(fileName = "SpawnerDefault", menuName = "Scriptable Objects/SpawnerDefault")]
public class SpawnerDefault : ScriptableObject
{
    [SerializeField] Gum _hitGum;
    [SerializeField] Gum _missGum;
    [SerializeField] float _radius = 1;
    [SerializeField] int _maxSpawnCount = 10;
    [SerializeField, Range(1, 10)] int _hitGumSpawnRate;
    [SerializeField, Range(1, 10)] int _hitGumSpawnHighRate;

    public Gum HitGum => _hitGum;
    public Gum MissGum => _missGum;
    public float Radius => _radius;
    public int MaxSpawnCount => _maxSpawnCount;
    public int HitGumSpawnRate => _hitGumSpawnRate;
    public int HitGumSpawnHighRate => _hitGumSpawnHighRate;
}

public class SpawnerRuntime
{
    SpawnerDefault _default;
    bool _isHighRate;
    int _hitGumSpawnRate;
    public int HitGumSpawnRate => _hitGumSpawnRate;

    public SpawnerRuntime(SpawnerDefault data)
    {
        _default = data;
        _hitGumSpawnRate = data.HitGumSpawnRate;
    }

    public bool HighRateActivation()
    {
        if (_isHighRate) return false;
        _isHighRate = true;
        _hitGumSpawnRate = _default.HitGumSpawnHighRate;
        return true;
    }

    public void HighRateSpawned()
    {
        if (_isHighRate)
        {
            _isHighRate = false;
            _hitGumSpawnRate = _default.HitGumSpawnRate;
        }
    }
}
