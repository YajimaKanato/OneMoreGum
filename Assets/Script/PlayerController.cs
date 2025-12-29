using UnityEngine;
using UnityEngine.Events;
using unityroom.Api;

public class PlayerController : MonoBehaviour, IPause, IResume, IGameOver
{
    [SerializeField] UnityEvent[] _events;
    Gum _target;
    Gum _preTarget;
    bool _isPause;
    bool _isGameOver;
    static PlayerSkill _skill;

    public void Init(PlayerSkill skill)
    {
        _skill = skill;
        GameFlowManager.Instance.RegisterList<IPause>(this);
        GameFlowManager.Instance.RegisterList<IResume>(this);
        GameFlowManager.Instance.RegisterList<IGameOver>(this);
        _isPause = false;
        _isGameOver = false;
    }

    public void Pause()
    {
        _isPause = true;
    }

    public void Resume()
    {
        _isPause = false;
    }

    public void GameOver()
    {
        var loadData = SaveManager.LoadDataPrefs<HitGumCounter>(HitGumCounter.FileName);
        if (loadData == null || loadData.HitCount < _skill.Score)
        {
            SaveManager.SaveDataPrefs(HitGumCounter.FileName, new HitGumCounter(_skill.Score));
        }
        UnityroomApiClient.Instance.SendScore(1, _skill.Score, ScoreboardWriteMode.HighScoreDesc);
        OutgameManager.Current = _skill.Score;
        _isGameOver = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isGameOver)
        {
            if (!_isPause)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out var hit))
                {
                    _preTarget = _target;
                    _target = hit.collider.gameObject.GetComponent<Gum>();
                    if (_preTarget != _target)
                    {
                        PlayerActionManager.Instance.SearchGum(preTarget: _preTarget);
                    }
                    if (_target && _target.tag == "Gum")
                    {
                        if (_target != _preTarget)
                        {
                            PlayerActionManager.Instance.SearchGum(_target);
                        }
                        if (Input.GetMouseButtonUp(0))
                        {
                            var index = _target.GumID + (_target.GumDefault.LottoType == GumDefault.Lotto.Hit || _skill.IsCertainHit ? 3 : 0);
                            PlayerActionManager.Instance.PurchaseGum(_target);
                            _events[7].Invoke();
                            _events[6].Invoke();
                            if (!Menu.Runtime.IsSkipMode)
                            {
                                _events[index].Invoke();
                                Debug.Log("Effect");
                            }
                            else
                            {
                                PlayerActionManager.Instance.StatusUpdate();
                                PlayerActionManager.Instance.GameOver();
                            }
                        }
                    }
                }
            }
        }
        else
        {
            PlayerActionManager.Instance.SearchGum(preTarget: _target);
        }
    }
}
