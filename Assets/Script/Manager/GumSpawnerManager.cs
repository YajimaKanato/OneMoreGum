using System.Collections.Generic;
using UnityEngine;

public class GumSpawnerManager : MonoBehaviour
{
    [SerializeField] GumSpawner[] _gumSpawner;
    SpawnerRuntime[] _spawnerRuntime;
    static GumSpawnerManager _instance;
    public static GumSpawnerManager Instance => _instance;
    public void Init()
    {
        if (_instance == null)
        {
            _instance = this;

            //Spawner
            var spawnerCount = _gumSpawner.Length;
            _spawnerRuntime = new SpawnerRuntime[spawnerCount];
            for (int i = 0; i < spawnerCount; i++)
            {
                _gumSpawner[i].Init();
                _spawnerRuntime[i] = _gumSpawner[i].Runtime;
            }

            ResetGums();
            GumSpawn();
        }
    }

    public void ResetGums()
    {
        foreach (var p in _gumSpawner)
        {
            p.ResetGums();
        }
    }

    public void GumSpawn()
    {
        foreach (var p in _gumSpawner)
        {
            p.GumSpawn();
            p.Runtime.HighRateSpawned();
        }
    }

    public int Perspective()
    {
        var hitCount = 0;
        foreach (var p in _gumSpawner)
        {
            foreach (var hitGum in p.HitGums)
            {
                if (hitGum) hitCount++;
            }
        }
        return hitCount;
    }

    public List<Gum> RevealHitGum()
    {
        var hitGumList = new List<Gum>();
        foreach (var spawner in _gumSpawner)
        {
            foreach (var gum in spawner.HitGums)
            {
                if (gum) hitGumList.Add(gum);
            }
        }
        return hitGumList;
    }

    public void HighRateMode()
    {
        var high = true;
        foreach (var spawner in _spawnerRuntime)
        {
            high = spawner.HighRateActivation();
        }
        if (high) Debug.Log("HighRateMode");
    }
}
