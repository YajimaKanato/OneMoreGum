using UnityEngine;
using UnityEngine.UI;

public class OutgameManager : GameManager
{
    [SerializeField] Text _highScore;

    static OutgameManager _instance;
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        HighScore();
    }

    void HighScore()
    {
        _highScore.text = "";
        var highScore = SaveManager.LoadDataPrefs<HitGumCounter>(HitGumCounter.FileName);
        if (highScore != null)
        {
            _highScore.text = "ハイスコア\n" + highScore.HitCount.ToString() + " 点";
        }
    }
}
