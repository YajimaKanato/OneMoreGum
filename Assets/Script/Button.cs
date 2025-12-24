using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
    [SerializeField] UnityEvent _event;
    bool _isOver;
    public void IsOver(bool isOver)
    {
        _isOver = isOver;
    }

    public void Act()
    {
        if (!_isOver) return;
        _event.Invoke();
    }
}
