using UnityEngine;
using UnityEngine.UI;

public class GumInfo : MonoBehaviour
{
    [SerializeField] Image _image;
    [SerializeField] Text _text;

    public void InfoUpdate(int rate, int point)
    {
        _text.text = "10個中\n" + rate.ToString("") + "個\n当たる\n" + point.ToString() + " 点";
    }
}
