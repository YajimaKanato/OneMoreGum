using System.Collections.Generic;
using UnityEngine;

public class GumSpawner : MonoBehaviour
{
    [SerializeField] SpawnerDefault _spawn;
    SpawnerRuntime _runtime;
    Gum _hitGum;
    Gum _missGum;
    float _radius;
    int _maxSpawnCount;
    public SpawnerDefault Spawn => _spawn;
    public SpawnerRuntime Runtime => _runtime;
    /// <summary>現在スポーン中のオブジェクトの配列</summary>
    Gum[] _gums;
    /// <summary>現在スポーン中のオブジェクトの中で当たりのものの配列</summary>
    Gum[] _hitGums;
    /// <summary>スポーンするオブジェクトに付与する番号のキュー</summary>
    Queue<int> _gumIDQueue;
    /// <summary>当たりガムのプール</summary>
    Queue<Gum> _hitGumPool;
    /// <summary>ハズレガムのプール</summary>
    Queue<Gum> _missGumPool;
    int _spawnCount;

    public Gum[] HitGums => _hitGums;
    public int MaxSpawnCount => _maxSpawnCount;

    public void Init()
    {
        //初期値
        _hitGum = _spawn.HitGum;
        _missGum = _spawn.MissGum;
        _radius = _spawn.Radius;
        _maxSpawnCount = _spawn.MaxSpawnCount;
        //new()
        _runtime = new SpawnerRuntime(_spawn);
        _gums = new Gum[_maxSpawnCount];
        _hitGums = new Gum[_maxSpawnCount];
        _gumIDQueue = new Queue<int>();
        _hitGumPool = new Queue<Gum>();
        _missGumPool = new Queue<Gum>();

        for (int i = 0; i < _maxSpawnCount; i++)
        {
            _gumIDQueue.Enqueue(i);
        }
    }

    public bool GumSpawn()
    {
        if (_spawnCount >= _maxSpawnCount) return false;
        while (_spawnCount < _maxSpawnCount)
        {
            //生成位置を決めるためのパラメータ
            var theta = Random.Range(0, 2 * Mathf.PI);
            var radius = Random.Range(0.1f, _radius);
            var pos = transform.position + new Vector3(Mathf.Cos(theta), 0, Mathf.Sin(theta)) * radius;
            var rot = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
            //当たりガムを生成するかどうか
            var rand = Random.Range(0, 10);
            //付与する番号
            var id = _gumIDQueue.Dequeue();
            Gum gum;
            if (rand < _runtime.HitGumSpawnRate)
            {
                if (_hitGumPool.Count > 0)
                {
                    gum = _hitGumPool.Dequeue();
                    gum.gameObject.SetActive(true);
                    gum.transform.position = pos;
                    gum.transform.rotation = Quaternion.Euler(rot);
                }
                else
                {
                    gum = Instantiate(_hitGum, pos, Quaternion.Euler(rot));
                }
                _hitGums[id] = gum;
            }
            else
            {
                if (_missGumPool.Count > 0)
                {
                    gum = _missGumPool.Dequeue();
                    gum.gameObject.SetActive(true);
                    gum.transform.position = pos;
                    gum.transform.rotation = Quaternion.Euler(rot);
                }
                else
                {
                    gum = Instantiate(_missGum, pos, Quaternion.Euler(rot));
                }
            }
            gum.SpawnSetting(this, id, _runtime.HitGumSpawnRate);
            _gums[id] = gum;
            _spawnCount++;
        }
        return true;
    }

    public void ReleaseToPool(Gum gum, int id)
    {
        _hitGums[id] = null;
        _gums[id] = null;
        _gumIDQueue.Enqueue(id);
        gum.gameObject.SetActive(false);
        if (gum.GumDefault.LottoType == GumDefault.Lotto.Hit)
        {
            _hitGumPool.Enqueue(gum);
        }
        else
        {
            _missGumPool.Enqueue(gum);
        }
        _spawnCount--;
    }

    public void ResetGums()
    {
        foreach (Gum gum in _gums)
        {
            if (gum) ReleaseToPool(gum, gum.ID);
        }
    }
}
