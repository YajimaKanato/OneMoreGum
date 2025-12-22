using UnityEngine;

public class IngameManager : MonoBehaviour
{
    [SerializeField] GameFlowManager _flow;
    [SerializeField] PlayerActionManager _action;

    private void Awake()
    {
        _flow.Init();
        _action.Init();
    }
}
