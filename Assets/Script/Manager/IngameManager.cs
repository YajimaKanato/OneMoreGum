using UnityEngine;

public class IngameManager : MonoBehaviour
{
    [SerializeField] GameFlowManager _flow;
    [SerializeField] PlayerActionManager _action;
    [SerializeField] GumSpawnerManager _gum;

    private void Awake()
    {
        _gum.Init();
        _flow.Init();
        _action.Init();
    }
}
