using UnityEngine;

public class OutgameManager : GameManager
{
    static OutgameManager _instance;
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }
}
