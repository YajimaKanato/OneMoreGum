using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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

    public void Click(Image image)
    {
        image.color = Color.gray;
    }

    public void Released(Image image)
    {
        image.color = Color.white;
    }
}
