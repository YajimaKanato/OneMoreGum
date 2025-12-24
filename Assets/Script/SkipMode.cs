using UnityEngine;
using UnityEngine.UI;

public class SkipMode : MonoBehaviour
{
    [SerializeField] Image _image;
    [SerializeField] Sprite _skip;
    [SerializeField] Sprite _noSkip;

    public void SkipModeActivation(bool skip)
    {
        _image.sprite = skip ? _skip : _noSkip;
    }
}
