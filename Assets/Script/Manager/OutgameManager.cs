using UnityEngine;
using UnityEngine.UI;

public class OutgameManager : GameManager
{
    [SerializeField] Text _current;
    [SerializeField] Text _highScore;
    public static int Current;
    static OutgameManager _instance;
    public static OutgameManager Instance => _instance;
    private void Start()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        if (_highScore)
            HighScore();
        if (_current)
            CurrentScore();
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

    void CurrentScore()
    {
        _current.text = "今回のスコア\n" + Current.ToString() + " 点";
    }
}
