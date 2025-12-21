using UnityEngine;

public class PlayerActionManager : MonoBehaviour
{
    [SerializeField] PlayerDefault _player;
    [SerializeField] PlayerController _playerController;
    [SerializeField] PlayerStatusUI _playerStatusUI;
    [SerializeField] Perspective _perspective;
    [SerializeField] GumSpawner _gumSpawner;
    PlayerSkill _skill;
    static PlayerActionManager _instance;
    public static PlayerActionManager Instance => _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            StatusUpdate();

            //Player
            _skill = new PlayerSkill(_player);
            _playerController.Init(_skill);

            //Spawner
            _gumSpawner.Init();
            ResetGums();
            GumSpawn();
        }
    }

    public void StatusUpdate()
    {
        _playerStatusUI.StatusUpdate(_skill);
    }

    #region Button
    public void ResetGums()
    {
        _gumSpawner.ResetGums();
    }

    public void GumSpawn()
    {
        _gumSpawner.GumSpawn();
    }

    public void Perspective()
    {
        if (!_skill.Perspective())
        {

        }
        else
        {
            var hitCount = 0;
            foreach (var gum in _gumSpawner.Gums)
            {
                if (gum)
                {
                    if (gum.GumDefault.LottoType == GumDefault.Lotto.Hit) hitCount++;
                }
            }
            _perspective.PerspectiveActivation(hitCount);
            StatusUpdate();
        }
    }
    #endregion
}
