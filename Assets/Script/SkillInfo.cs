using UnityEngine;
using UnityEngine.UI;

public class SkillInfo : MonoBehaviour
{
    [SerializeField] PlayerSkillData _player;
    [SerializeField] SkillData _skillData;
    [SerializeField] Text _text;

    public void InfoUpdate(bool discount)
    {
        var cost = _skillData.cost - (discount && _skillData.Type == GumDefault.Lotto.Miss ? _player.DiscountCost : 0);
        _text.text = _skillData.Info + "\n必要な" + (_skillData.Type == GumDefault.Lotto.Hit ? "当たり" : "ハズレ") + "ガム\n" + cost + " 個";
    }
}
