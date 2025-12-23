using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class GameManager : MonoBehaviour
{
    public void SceneChange(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
