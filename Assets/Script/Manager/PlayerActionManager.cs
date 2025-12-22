using UnityEngine;
using System.Collections.Generic;

public class PlayerActionManager : MonoBehaviour
{
    [SerializeField] PlayerDefault _player;
    [SerializeField] PlayerController _playerController;
    [SerializeField] PlayerStatusUI _playerStatusUI;
    [SerializeField] Perspective _perspective;
    [SerializeField] GumSpawner[] _gumSpawner;
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
            foreach (var p in _gumSpawner)
            {
                p.Init();
            }
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
        }
    }

    public void Perspective()
    {
        if (!_skill.Perspective())
        {

        }
        else
        {
            var hitCount = 0;
            foreach (var p in _gumSpawner)
            {
                foreach (var gum in p.Gums)
                {
                    if (gum)
                    {
                        if (gum.GumDefault.LottoType == GumDefault.Lotto.Hit) hitCount++;
                    }
                }
            }
            _perspective.PerspectiveActivation(hitCount);
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
            var hitGumList = new List<Gum>();
            foreach (var spawner in _gumSpawner)
            {
                foreach (var gum in spawner.Gums)
                {
                    if (gum && gum.GumDefault.LottoType == GumDefault.Lotto.Hit) hitGumList.Add(gum);
                }
            }

            var revealIndex = 0;
            for (int i = 0; i < _skill.RevealGumCount; i++)
            {
                revealIndex += Random.Range(0, hitGumList.Count);
                revealIndex %= hitGumList.Count;
                hitGumList[revealIndex].Reveal();
                hitGumList.RemoveAt(revealIndex);
            }
            StatusUpdate();
            Debug.Log("RevealHitGum");
        }
    }
    #endregion
}
