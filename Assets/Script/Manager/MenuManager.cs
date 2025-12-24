using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] MenuData _menuData;
    [SerializeField] Menu _menu;
    [SerializeField] GameObject _filter;
    [SerializeField] float _filterTime = 0.05f;
    Coroutine _filterCoroutine;

    private void Awake()
    {
        _menu.Init(_menuData);
        _filter?.SetActive(false);
        MenuClose();
    }

    public void MenuOpen()
    {
        _menu.OpenSetting();
    }

    public void MenuClose()
    {
        _menu.MenuClose();
    }

    public void Filter()
    {
        if (_filterCoroutine != null)
        {
            StopCoroutine(_filterCoroutine);
            _filterCoroutine = null;
        }
        _filterCoroutine = StartCoroutine(FilterCoroutine());
        Debug.Log("Filter ON");
    }

    IEnumerator FilterCoroutine()
    {
        _filter.SetActive(true);
        yield return new WaitForSeconds(_filterTime);
        _filter.SetActive(false);
    }

    public void SkipMode()
    {
        _menu.IsSkipMode();
    }
}
