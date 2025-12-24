using UnityEngine;

public class SkillUI : MonoBehaviour
{
    [SerializeField] SkillButton[] _buttons;
    int _preIndex;
    int _currentIndex = 0;
    bool _discountMode;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _preIndex = _currentIndex;
        for (int i = 0; i < _buttons.Length; i++)
        {
            _buttons[i].SkillInfoSetting(_discountMode);
            _buttons[i].gameObject.SetActive(i == _currentIndex);
        }
    }

    public void NextSkill()
    {
        _preIndex = _currentIndex;
        _currentIndex++;
        _currentIndex %= _buttons.Length;
        _buttons[_preIndex].gameObject.SetActive(false);
        _buttons[_currentIndex].gameObject.SetActive(true);
        _buttons[_currentIndex].SkillInfoSetting(_discountMode);
    }

    public void BackSkill()
    {
        _preIndex = _currentIndex;
        _currentIndex--;
        if (_currentIndex < 0) _currentIndex = _buttons.Length - 1;
        _buttons[_preIndex].gameObject.SetActive(false);
        _buttons[_currentIndex].gameObject.SetActive(true);
        _buttons[_currentIndex].SkillInfoSetting(_discountMode);
    }

    public void DiscountModeActivation()
    {
        _discountMode = true;
        _buttons[_currentIndex].SkillInfoSetting(_discountMode);
    }

    public void DiscountModeDeactivation()
    {
        _discountMode = false;
        _buttons[_currentIndex].SkillInfoSetting(_discountMode);
    }
}
