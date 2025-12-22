using UnityEngine;
using UnityEngine.UI;

public class GumInfo : MonoBehaviour
{
    [SerializeField] Image _image;
    [SerializeField] Text _text;

    public void InfoUpdate(int rate)
    {
        _text.text = "当たりの確率\n" + rate.ToString("00") + " %";
    }
}
