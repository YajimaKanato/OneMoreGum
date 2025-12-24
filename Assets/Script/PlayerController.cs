using UnityEngine;

public class PlayerController : MonoBehaviour, IPause, IResume, IGameOver
{
    Gum _target;
    Gum _preTarget;
    bool _isPause;
    bool _isGameOver;

    public void Init()
    {
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
