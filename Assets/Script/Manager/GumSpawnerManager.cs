using System.Collections.Generic;
using UnityEngine;

public class GumSpawnerManager : MonoBehaviour
{
    [SerializeField] GumSpawner[] _gumSpawner;
    SpawnerRuntime[] _spawnerRuntime;
    static GumSpawnerManager _instance;
    public static GumSpawnerManager Instance => _instance;
    public void Start()
    {
        if (_instance == null)
        {
            Debug.Log("GumSpawnerManager");
            _instance = this;

            //Spawner
            var spawnerCount = _gumSpawner.Length;
            if (_gumSpawner == null || spawnerCount == 0)
            {
                //Debug.LogError("GumSpawner not found");
                return;
            }
            _spawnerRuntime = new SpawnerRuntime[spawnerCount];
            for (int i = 0; i < spawnerCount; i++)
            {
                _gumSpawner[i]?.Init();
                _spawnerRuntime[i] = _gumSpawner[i].Runtime;
            }

            ResetGums();
            GumSpawn();
        }
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }

    public void ResetGums()
    {
        foreach (var p in _gumSpawner)
        {
            p.ResetGums();
        }
    }

    public bool GumSpawn()
    {
        var spawned = false;
        foreach (var p in _gumSpawner)
        {
            spawned |= p.GumSpawn();
            p.Runtime.HighRateSpawned();
        }
        if (spawned && PlayerActionManager.Instance) PlayerActionManager.Instance.HighRateModeDeactivation();
        return spawned;
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
                if (gum && !gum.IsRevealed) hitGumList.Add(gum);
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
    }

    public void PointUPMode()
    {
        foreach (var spawner in _gumSpawner)
        {
            spawner.MagnifUp();
        }
    }

    public void PointUPModeDeactivaion()
    {
        foreach (var spawner in _gumSpawner)
        {
            spawner.DefaultMagnif();
        }
    }
}
