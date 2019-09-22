using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseUI;
    public SceneFader sceneFader;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
            Toggle();
    }

    public void Toggle()
    {
        pauseUI.SetActive(!pauseUI.activeSelf);
        Time.timeScale = pauseUI.activeSelf ? 0f : 1f;
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        sceneFader.FadeToScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        sceneFader.FadeToScene(GameManager.Scenes.Menu);
    }
}
