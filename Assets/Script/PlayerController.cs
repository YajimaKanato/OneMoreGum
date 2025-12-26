using UnityEngine;
using UnityEngine.Events;

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
                            PlayerActionManager.Instance.PurchaseGum(_target);
                            _events[_target.GumID].Invoke();
                            _events[_target.GumDefault.LottoType == GumDefault.Lotto.Hit ? 3 : 4].Invoke();
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

    private void OnDisable()
    {
        GameFlowManager.Instance.RemoveData<IPause>(this);
        GameFlowManager.Instance.RemoveData<IResume>(this);
        GameFlowManager.Instance.RemoveData<IGameOver>(this);
    }
}
