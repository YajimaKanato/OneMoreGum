using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerActionManager : MonoBehaviour
{
    [SerializeField] UnityEvent[] _events;
    [SerializeField] PlayerDefault _player;
    [SerializeField] PlayerController _playerController;
    [SerializeField] PlayerStatusUI _playerStatusUI;
    [SerializeField] Perspective _perspective;
    [SerializeField] SearchUI _searchUI;
    [SerializeField] Transform _boxTrans;
    [SerializeField] SkillUI _skillUI;
    PlayerSkill _skill;
    GumSpawnerManager _gumSpawnerManager;
    static PlayerActionManager _instance;
    public static PlayerActionManager Instance => _instance;

    //public void Start()
    //{
    //    if (_instance == null)
    //    {
    //        _instance = this;
    //        StatusUpdate();

    //        //Player
    //        _skill = new PlayerSkill(_player);
    //        _playerController.Init(_skill);
    //    }

    //    _gumSpawnerManager = GumSpawnerManager.Instance;
    //    Debug.Log("PlayerActionManager" + _gumSpawnerManager);
    //}

    private IEnumerator Start()
    {
        yield return null;
        if (_instance == null)
        {
            _instance = this;
            StatusUpdate();

            //Player
            _skill = new PlayerSkill(_player);
            _playerController.Init(_skill);
        }

        _gumSpawnerManager = GumSpawnerManager.Instance;
        if (_gumSpawnerManager == null)
            Debug.LogError("GumSpawnerManager not ready");
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
            _searchUI.DrawLine(target, _boxTrans.position);
        }
    }

    public void PurchaseGum(Gum gum)
    {
        if (_skill.PurchaseGum(gum.GumDefault.GumValue))
        {
            StatusUpdate();
            if (gum.OpenLotto(_skill.IsCertainHit, out var score) == GumDefault.Lotto.Hit)
            {
                _skill.GetHitGum(score);
            }
            else
            {
                _skill.GetMissGum();
            }
            _skill.CertainHitModeDeactivation();
            //StatusUpdate();
            Debug.Log("Purchase Success");
        }
        else
        {
            Debug.Log("Purchase Failed");
        }
        
        if (_skill.RateUpCount <= 0)
        {
            _skill.PointUPModeDeactivation();
            _gumSpawnerManager.PointUPModeDeactivaion();
            Debug.Log("PointUPMode Deactivaion");
        }
        Debug.Log($"Money => {_skill.CurrentMoney}");
        Debug.Log($"HitCount => {_skill.HitCount}");
    }

    public void GameOver()
    {
        if (!_skill.IsPurchasable) GameFlowManager.Instance.GameOver();
    }

    void DiscountModeDeactivation()
    {
        _skill.DiscountModeDeactivation();
        _skillUI.DiscountModeDeactivation();
    }

    public void HighRateModeDeactivation()
    {
        _skill.HighRateModeDeactivation();
    }

    #region Skill
    public void Perspective(int index)
    {
        if (!_skill.Perspective())
        {

        }
        else
        {
            _events[index].Invoke();
            _perspective.PerspectiveActivation(_gumSpawnerManager.Perspective());
            DiscountModeDeactivation();
            StatusUpdate();
            Debug.Log("Perspective");
        }
    }

    public void CertainHitMode(int index)
    {
        if (_skill.CertainHitModeActivation())
        {
            _events[index].Invoke();
            StatusUpdate();
            DiscountModeDeactivation();
            Debug.Log("CertainHitMode");
        }
    }

    public void RevealHitGum(int index)
    {
        if (_skill.RevealHitGum())
        {
            var hitGumList = _gumSpawnerManager.RevealHitGum();
            var revealIndex = 0;
            if (hitGumList.Count > 0)
            {
                for (int i = 0; i < _skill.RevealGumCount; i++)
                {
                    revealIndex += Random.Range(0, hitGumList.Count);
                    revealIndex %= hitGumList.Count;
                    hitGumList[revealIndex].Reveal();
                    hitGumList.RemoveAt(revealIndex);
                    if (hitGumList.Count == 0) break;
                }
            }
            _events[index].Invoke();
            DiscountModeDeactivation();
            StatusUpdate();
            Debug.Log("RevealHitGum");
        }
    }

    public void HighRateMode(int index)
    {
        if (_skill.HighRateModeActivation())
        {
            _events[index].Invoke();
            _gumSpawnerManager.HighRateMode();
            DiscountModeDeactivation();
            StatusUpdate();
            Debug.Log("HighRateMode Activation");
        }
    }

    public void NRHRMode(int index)
    {
        if (_skill.NRHRModeActivation())
        {
            _events[index].Invoke();
            DiscountModeDeactivation();
            StatusUpdate();
            Debug.Log("NRHRMode Activation");
        }
    }

    public void PointUPMode(int index)
    {
        if (_skill.PointUPModeActivation())
        {
            _events[index].Invoke();
            _gumSpawnerManager.PointUPMode();
            DiscountModeDeactivation();
            StatusUpdate();
            Debug.Log("RateUPMode Activation");
        }
    }

    public void DiscountMode(int index)
    {
        if (_skill.DiscountModeActivation())
        {
            _events[index].Invoke();
            _skillUI.DiscountModeActivation();
            StatusUpdate();
            Debug.Log("DiscountMode Activation");
        }
    }
    #endregion
}
