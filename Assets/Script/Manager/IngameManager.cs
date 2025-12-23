using UnityEngine;

public class IngameManager : GameManager
{
    [SerializeField] GameFlowManager _flow;
    [SerializeField] PlayerActionManager _action;
    [SerializeField] GumSpawnerManager _gum;
    [SerializeField] TimeManager _time;

    private void Awake()
    {
        _flow.Init();
        _gum.Init();
        _action.Init();
        _time.Init();
    }
}
