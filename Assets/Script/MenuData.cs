using UnityEngine;

[CreateAssetMenu(fileName = "MenuData", menuName = "Scriptable Objects/MenuData")]
public class MenuData : ScriptableObject
{
    [SerializeField, Range(0, 1)] float _bgmVolume = 1.0f;
    [SerializeField, Range(0, 1)] float _seVolume = 1.0f;
    [SerializeField] bool _isSkipMode;

    public float BGMVolume => _bgmVolume;
    public float SEVolume => _seVolume;
    public bool IsSkipMode => _isSkipMode;
}

public class MenuRuntime
{
    MenuData _menu;
    float _bgmVolume;
    float _seVolume;
    bool _isSkipMode;
    public float BGMVolume => _bgmVolume;
    public float SEVolume => _seVolume;
    public bool IsSkipMode => _isSkipMode;

    public MenuRuntime(MenuData data)
    {
        _menu = data;
        _bgmVolume = _menu.BGMVolume;
        _seVolume = _menu.SEVolume;
    }

    public void BGMVolumeChange(float volume)
    {
        _bgmVolume = volume;
    }

    public void SEVolumeChange(float volume)
    {
        _seVolume = volume;
    }

    public void IsSkipModeActivation()
    {
        _isSkipMode = !_isSkipMode;
    }
}
