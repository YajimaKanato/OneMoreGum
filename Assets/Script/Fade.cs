using UnityEngine;
using UnityEngine.Events;

public class Fade : MonoBehaviour
{
    [SerializeField] UnityEvent[] _events;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void EventInvoke(int index)
    {
        _events[index].Invoke();
    }
}
