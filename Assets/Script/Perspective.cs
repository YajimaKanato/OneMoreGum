using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Perspective : MonoBehaviour
{
    [SerializeField] UnityEvent _start;
    [SerializeField] UnityEvent _end;
    [SerializeField] Text _hitCount;

    public void PerspectiveActivation(int hitCount)
    {
        Debug.Log($"当たりガムの個数 => {hitCount}");
        _hitCount.text = hitCount.ToString() + "個の当たりガムがありそうだ";
        _start.Invoke();
    }

    public void StopPerspective()
    {
        _end.Invoke();
    }
}
