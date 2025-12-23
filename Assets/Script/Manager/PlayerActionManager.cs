using UnityEngine;
using System.Collections.Generic;

public class PlayerActionManager : MonoBehaviour
{
    [SerializeField] PlayerDefault _player;
    [SerializeField] PlayerController _playerController;
    [SerializeField] PlayerStatusUI _playerStatusUI;
    [SerializeField] Perspective _perspective;
    [SerializeField] SearchUI _searchUI;
    [SerializeField] Transform _boxTrans;
    PlayerSkill _skill;
    GumSpawnerManager _gumSpawnerManager;
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
        }

        _gumSpawnerManager = GumSpawnerManager.Instance;
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
            if (gum.OpenLotto(_skill.IsCertainHit) == GumDefault.Lotto.Hit)
            {
                _skill.GetHitGum();
            }
            else
            {
                _skill.GetMissGum();
            }
            _skill.CertainHitModeDeactivation();
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

    #region Skill
    public void ResetGums()
    {
        _gumSpawnerManager.ResetGums();
    }

    public void GumSpawn()
    {
        if (!_gumSpawnerManager.GumSpawn()) return;
        if (_skill.IsHighRate)
        {
            _skill.HighRateModeDeactivaion();
            Debug.Log("HighRateMode Deactivation");
        }
    }

    public void Perspective()
    {
        if (!_skill.Perspective())
        {

        }
        else
        {
            _perspective.PerspectiveActivation(_gumSpawnerManager.Perspective());
            StatusUpdate();
            Debug.Log("Perspective");
        }
    }

    public void CertainHitMode()
    {
        if (_skill.CertainHitModeActivation())
        {
            StatusUpdate();
            Debug.Log("CertainHitMode");
        }
    }

    public void RevealHitGum()
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
            StatusUpdate();
            Debug.Log("RevealHitGum");
        }
    }

    public void HighRateMode()
    {
        if (_skill.HighRateModeActivation())
        {
            _gumSpawnerManager.HighRateMode();
            StatusUpdate();
            Debug.Log("HighRateMode Activation");
        }
    }

    public void NRHRMode()
    {
        if (_skill.NRHRModeActivation())
        {
            StatusUpdate();
            Debug.Log("NRHRMode Activation");
        }
    }
    #endregion
}
