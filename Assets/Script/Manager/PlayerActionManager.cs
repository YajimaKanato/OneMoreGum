using UnityEngine;

public class PlayerActionManager : MonoBehaviour
{
    [SerializeField] PlayerDefault _player;
    [SerializeField] PlayerController _playerController;
    [SerializeField] PlayerStatusUI _playerStatusUI;
    [SerializeField] Perspective _perspective;
    [SerializeField] GumSpawner _gumSpawner;
    [SerializeField] SearchUI _searchUI;
    [SerializeField] Transform _boxTrans;
    PlayerSkill _skill;
    static PlayerActionManager _instance;
    public static PlayerActionManager Instance => _instance;

    public void Init()
    {
        if (_instance == null)
        {
            _instance = this;
            StatusUpdate();

            //Player
            _skill = new PlayerSkill(_player);
            _playerController.Init();

            //Spawner
            _gumSpawner.Init();
            ResetGums();
            GumSpawn();
        }
    }

    public void StatusUpdate()
    {
        _playerStatusUI.StatusUpdate(_skill);
    }

    public void SearchGum(Gum target = null, Gum preTarget = null)
    {
        if (preTarget)
        {
            preTarget?.EscapedObserving();
            _searchUI.EraseLine();
        }
        if (target)
        {
            target.Observing();
            _searchUI.DrawLine(target.transform.position, _boxTrans.position);
        }
    }

    public void PurchaseGum(Gum gum)
    {
        if (_skill.PurchaseGum(gum.GumDefault.GumValue))
        {
            if (gum.OpenLotto() == GumDefault.Lotto.Hit)
            {
                _skill.GetHitGum();
            }
            else
            {
                _skill.GetMissGum();
            }
            StatusUpdate();
            Debug.Log("Purchase Success");
        }
        else
        {
            Debug.Log("Purchase Failed");
        }
        if (!_skill.IsPurchasable) GameFlowManager.Instance.GameOver();
        Debug.Log($"Money => {_skill.CurrentMoney}");
        Debug.Log($"HitCount => {_skill.HitCount}");
    }

    #region Button
    public void ResetGums()
    {
        _gumSpawner.ResetGums();
    }

    public void GumSpawn()
    {
        _gumSpawner.GumSpawn();
    }

    public void Perspective()
    {
        if (!_skill.Perspective())
        {

        }
        else
        {
            var hitCount = 0;
            foreach (var gum in _gumSpawner.Gums)
            {
                if (gum)
                {
                    if (gum.GumDefault.LottoType == GumDefault.Lotto.Hit) hitCount++;
                }
            }
            _perspective.PerspectiveActivation(hitCount);
            StatusUpdate();
        }
    }
    #endregion
}
