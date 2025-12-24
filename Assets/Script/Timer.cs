using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] Image _image;

    public void TextUpdate(float delta)
    {
        _image.fillAmount = delta;
    }
}
