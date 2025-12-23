using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] MenuData _menuData;
    [SerializeField] Menu _menu;
    static MenuRuntime _runtime;


    private void Awake()
    {
        if (_runtime == null)
        {
            _runtime = new MenuRuntime(_menuData);
        }
        MenuClose();
    }

    public void MenuOpen()
    {
        _menu.gameObject.SetActive(true);
    }

    public void MenuClose()
    {
        _menu.gameObject.SetActive(false);
    }
}
