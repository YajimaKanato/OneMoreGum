using UnityEngine;
using UnityEngine.UI;

public class SkillInfo : MonoBehaviour
{
    [SerializeField] PlayerSkillData _player;
    [SerializeField] SkillData _skillData;
    Text _text;

    private void Awake()
    {
        _text = GetComponent<Text>();
    }

    public void InfoUpdate(bool discount)
    {
        var cost = _skillData.cost - (discount && _skillData.Type == GumDefault.Lotto.Miss ? _player.DiscountCost : 0);
        _text.text = _skillData.Info + "\n必要な" + (_skillData.Type == GumDefault.Lotto.Hit ? "<color=red>当たり</color>" : "<color=blue>ハズレ</color>") + "ガム\n" + cost + " 個";
    }
}
