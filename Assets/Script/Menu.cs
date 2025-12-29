using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject[] _objs;
    [SerializeField] AudioSource _bgm;
    [SerializeField] AudioSource _se;
    [SerializeField] Slider _bgmSlider;
    [SerializeField] Slider _seSlider;
    [SerializeField] SkipMode _skipMode;
    static MenuRuntime _runtime;
    public static MenuRuntime Runtime => _runtime;

    public void Init(MenuData data)
    {
        if (_runtime == null)
        {
            _runtime = new MenuRuntime(data);
        }
        if (_bgmSlider) _bgmSlider.value = _runtime.BGMVolume;
        _bgm.volume = _runtime.BGMVolume;
        if (_seSlider) _seSlider.value = _runtime.SEVolume;
        _se.volume = _runtime.SEVolume;
        if (_skipMode) _skipMode.SkipModeActivation(_runtime.IsSkipMode);
    }

    public void OpenSetting()
    {
        foreach (var obj in _objs)
        {
            obj.SetActive(true);
        }
    }

    public void MenuClose()
    {
        foreach (var obj in _objs)
        {
            obj.SetActive(false);
        }
    }

    public void BGMVolumeChange()
    {
        _runtime.BGMVolumeChange(_bgmSlider.value);
        _bgm.volume = _runtime.BGMVolume;
    }

    public void SEVolumeChange()
    {
        _runtime.SEVolumeChange(_seSlider.value);
        _se.volume = _runtime.SEVolume;
    }

    public void IsSkipMode()
    {
        _runtime.IsSkipModeActivation();
        _skipMode.SkipModeActivation(_runtime.IsSkipMode);
        Debug.Log($"SkipMode => {_runtime.IsSkipMode}");
    }
}
