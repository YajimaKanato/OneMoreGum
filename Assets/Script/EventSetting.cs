using UnityEngine;
using UnityEngine.Events;

public class EventSetting : MonoBehaviour
{
    [SerializeField] UnityEvent[] _events;

    public void EventInvoke(int index)
    {
        Debug.Log("Event Invoke");
        _events[index].Invoke();
    }
}
