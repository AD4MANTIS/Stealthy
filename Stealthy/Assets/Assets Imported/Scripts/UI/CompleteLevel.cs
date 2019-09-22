using UnityEngine;

public class CompleteLevel : MonoBehaviour
{
    public SceneFader sceneFader;

    private GameManager.Scenes currentScene;

    private void OnEnable()
    {
        currentScene = GameManager.Instance.scene;
        if (PlayerPrefs.GetInt("levelReached") <= currentScene - GameManager.Scenes.Lvl00)
            PlayerPrefs.SetInt("levelReached", currentScene - GameManager.Scenes.Lvl00 + 1);
    }

    public void Continue()
    {
        GameManager.Instance.LoadLevel(currentScene + 1);
    }

    public void Menu() => GameManager.Instance.LoadMenu();
}
