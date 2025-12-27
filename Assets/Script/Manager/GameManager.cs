using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public abstract class GameManager : MonoBehaviour
{
    public void SceneChange(string sceneName)
    {
        Debug.Log("SceneChange called: " + sceneName);
        SceneManager.LoadScene(sceneName);
    }
}
