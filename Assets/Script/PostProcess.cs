using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcess : MonoBehaviour, IPause, IResume
{
    [SerializeField] GameObject _skillFilter;
    [SerializeField] Volume _volume;
    [SerializeField] UnityEvent[] _events;
    IEnumerator _coroutine;

    public void Pause()
    {
        StopCoroutine(_coroutine);
    }

    public void Resume()
    {
        StartCoroutine(_coroutine);
    }

    public void Vignette(int index)
    {
        if (_volume.profile.TryGet(out Vignette vignette))
        {
            _coroutine = VignetteCoroutine(index, vignette);
            StartCoroutine(_coroutine);
            Debug.Log("Vignette");
        }
    }

    IEnumerator VignetteCoroutine(int index, Vignette vignette)
    {
        _events[index].Invoke();
        _skillFilter.SetActive(true);
        vignette.center.value = new Vector2(0.88f, 0.85f);
        var intensity = vignette.intensity.value;
        while (intensity < 0.5)
        {
            intensity += 1 / 200f;
            vignette.intensity.value = intensity;
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        while (intensity > 0)
        {
            intensity -= 1 / 350f;
            vignette.intensity.value = intensity;
            yield return null;
        }
        _skillFilter.SetActive(false);
    }
}
