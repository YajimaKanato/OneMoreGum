using System.Collections;
using UnityEngine;

public class SearchUI : MonoBehaviour
{
    [SerializeField] GumInfo _gumInfo;

    private void Start()
    {
        _gumInfo.gameObject.SetActive(false);
    }

    public void DrawLine(Gum gum, Vector3 boxPos)
    {
        _gumInfo.gameObject.SetActive(true);
        _gumInfo.InfoUpdate(gum.HitGumRate, gum.Score);
    }

    public void EraseLine()
    {
        _gumInfo.gameObject.SetActive(false);
    }
}
