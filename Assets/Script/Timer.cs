using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] Text _text;

    public void TextUpdate(float delta)
    {
        _text.text = delta.ToString("00.0");
    }
}
