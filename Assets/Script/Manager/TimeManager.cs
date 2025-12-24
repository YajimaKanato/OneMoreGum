using UnityEngine;

public class TimeManager : MonoBehaviour, IPause, IResume, IGameOver
{
    [SerializeField, Range(1, 60)] int _spawnInterval = 30;
    [SerializeField] Timer _timer;
    float _delta;
    bool _isPause;
    bool _isGameOver;

    public float Delta => _delta;

    public void Init()
    {
        GameFlowManager.Instance.RegisterList<IPause>(this);
        GameFlowManager.Instance.RegisterList<IResume>(this);
        GameFlowManager.Instance.RegisterList<IGameOver>(this);
        _delta = _spawnInterval;
        _isPause = false;
        _isGameOver = false;
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

    private void Update()
    {
        if (!_isGameOver)
        {
            if (!_isPause)
            {
                _delta -= Time.deltaTime;
                if (_delta <= 0)
                {
                    _delta = _spawnInterval;
                    if (GumSpawnerManager.Instance.GumSpawn()) Debug.Log("GumRespawn");
                }
                _timer.TextUpdate(_delta / _spawnInterval);
            }
        }
    }
}
