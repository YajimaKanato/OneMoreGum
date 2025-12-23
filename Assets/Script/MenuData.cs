using UnityEngine;

[CreateAssetMenu(fileName = "MenuData", menuName = "Scriptable Objects/MenuData")]
public class MenuData : ScriptableObject
{
    [SerializeField] float _bgmVolume = 1.0f;
    [SerializeField] float _seVolume = 1.0f;

    public float BGMVolume => _bgmVolume;
    public float SEVolume => _seVolume;
}

public class MenuRuntime
{
    MenuData _menu;
    float _bgmVolume;
    float _seVolume;
    public MenuRuntime(MenuData data)
    {
        _menu = data;
        _bgmVolume = _menu.BGMVolume;
        _seVolume = _menu.SEVolume;
    }

    public void BGMVolume(float volume)
    {
        _bgmVolume = volume;
    }

    public void SEVolume(float volume)
    {
        _seVolume = volume;
    }
}
