using System.Collections;
using UnityEngine;

public class IngameManager : GameManager
{
    [SerializeField] GameFlowManager _flow;
    [SerializeField] PlayerActionManager _action;
    [SerializeField] GumSpawnerManager _gum;
    [SerializeField] TimeManager _time;

    //private IEnumerator Start()
    //{
    //    _flow.Init();
    //    yield return null;
    //    _gum.Init();
    //    _action.Init();
    //    _time.Init();
    //}
}
