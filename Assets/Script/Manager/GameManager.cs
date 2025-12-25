using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public abstract class GameManager : MonoBehaviour
{
    [SerializeField] UnityEvent[] _events;
    int _eventIndex = 0;
    public void SceneChange(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void EventInvoke()
    {
        if (_eventIndex > _events.Length - 1) return;
        Debug.Log("Push Click");
        _events[_eventIndex].Invoke();
        _eventIndex++;
    }
}
