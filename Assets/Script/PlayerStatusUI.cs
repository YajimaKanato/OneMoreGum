using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusUI : MonoBehaviour
{
    [SerializeField] PlayerDefault _player;
    [SerializeField] Text _moneyText;
    [SerializeField] Text _hitText;
    [SerializeField] Text _totalHitText;
    [SerializeField] Text _missText;

    public void StatusUpdate(PlayerSkill skill)
    {
        _moneyText.text = "所持金 : " + (skill != null ? skill.CurrentMoney.ToString("") : _player.DefaultMoney) + " 円";
        _hitText.text = "当たりガムの個数 : " + (skill != null ? skill.HitCount.ToString("") : _player.DefaultHitCount) + " 個";
        _totalHitText.text = "スコア : " + (skill != null ? skill.Score.ToString("") : _player.DefaultTotalHitCount) + " 点";
        _missText.text = "ハズレガムの個数 : " + (skill != null ? skill.MissCount.ToString("") : _player.DefaultMissCount) + " 個";
    }
}
