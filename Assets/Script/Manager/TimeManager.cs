using UnityEngine;
using System.Collections;

public class TimeManager : MonoBehaviour, IPause, IResume, IGameOver
{
    [SerializeField, Range(1, 60)] int _spawnInterval = 30;
    [SerializeField] Timer _timer;
    float _delta;
    bool _isPause;
    bool _isGameOver;

    public float Delta => _delta;

    bool _initialized;

    private IEnumerator Start()
    {
        // ★ WebGL 最重要
        yield return null;

        if (GameFlowManager.Instance == null)
        {
            Debug.LogError("GameFlowManager not found");
            yield break;
        }

        GameFlowManager.Instance.RegisterList<IPause>(this);
        GameFlowManager.Instance.RegisterList<IResume>(this);
        GameFlowManager.Instance.RegisterList<IGameOver>(this);

        _delta = _spawnInterval;
        _initialized = true;
    }


    public void GameOver()
    {
        _isGameOver = true;
    }

    public void Pause()
    {
        _isPause = true;
    }

    public void Resume()
    {
        _isPause = false;
    }

    //private void Update()
    //{
    //    if (!_isGameOver)
    //    {
    //        if (!_isPause)
    //        {
    //            _delta -= Time.deltaTime;
    //            if (_delta <= 0)
    //            {
    //                _delta = _spawnInterval;
    //                if (GumSpawnerManager.Instance.GumSpawn()) Debug.Log("GumRespawn");
    //            }
    //            _timer.TextUpdate(_delta / _spawnInterval);
    //        }
    //    }
    //}

    private void Update()
    {
        if (!_initialized) return;
        if (_isGameOver || _isPause) return;
        if (GumSpawnerManager.Instance == null) return;

        _delta -= Time.deltaTime;
        if (_delta <= 0)
        {
            _delta = _spawnInterval;
            GumSpawnerManager.Instance.GumSpawn();
        }
        _timer.TextUpdate(_delta / _spawnInterval);
    }

}
