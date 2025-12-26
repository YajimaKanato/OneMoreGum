using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcess : MonoBehaviour
{
    [SerializeField] Volume _volume;
    [SerializeField] UnityEvent[] _events;
    public void Vignette(int index)
    {
        if (_volume.profile.TryGet(out Vignette vignette))
        {
            StartCoroutine(VignetteCoroutine(index, vignette));
            Debug.Log("Vignette");
        }
    }

    IEnumerator VignetteCoroutine(int index, Vignette vignette)
    {
        var intensity = vignette.intensity.value;
        while (intensity < 0.5)
        {
            intensity += 1 / 200f;
            vignette.intensity.value = intensity;
            yield return null;
        }
        _events[index].Invoke();
        yield return new WaitForSeconds(0.5f);
        while (intensity > 0)
        {
            intensity -= 1 / 350f;
            vignette.intensity.value = intensity;
            yield return null;
        }
    }
}
