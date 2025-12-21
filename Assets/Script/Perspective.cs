using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Perspective : MonoBehaviour
{
    [SerializeField] Text _hitCount;
    [SerializeField] float _effectiveTime = 1f;
    Coroutine _coroutine;

    public void PerspectiveActivation(int hitCount)
    {
        _hitCount.text = hitCount.ToString() + "個の当たりガムがありそうだ";
        Debug.Log($"当たりガムの個数 => {hitCount}");
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
        _coroutine = StartCoroutine(PerspectiveCoroutine(hitCount));
    }

    IEnumerator PerspectiveCoroutine(int hitCount)
    {
        yield return new WaitForSeconds(_effectiveTime);
        _hitCount.text = "";
    }
}
