using UnityEngine;

public class SkillButton : Button
{
    [SerializeField] SkillInfo _info;

    public void SkillInfoSetting(bool discount)
    {
        _info.InfoUpdate(discount);
    }
}
