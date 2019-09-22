using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public SceneFader sceneFader;

    public void Retry() => sceneFader.FadeToScene(SceneManager.GetActiveScene().buildIndex);

    public void Menu() => sceneFader.FadeToScene(GameManager.Scenes.Menu);
}
