using nvp.events;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum Scenes : int
    {
        Null = -1, Menu = 0, LevelSelect, Lvl00, Lvl01
    }

    #region Singleton

    public static GameManager Instance { get; private set; }

    public GameObject Enemies;

    private void Awake()
    {
        if (Instance)
            Debug.LogError("More than one Singleton Object");
        else
        {
            Instance = this;
            scene = (Scenes)SceneManager.GetActiveScene().buildIndex;
            NvpEventController.Events(MyEvent.PlayerDies).GameEventHandler += GameOver;
            NvpEventController.Events(MyEvent.LevelFinish).GameEventHandler += WinLevel;
        }
    }

    private void OnDisable()
    {
        NvpEventController.Events(MyEvent.PlayerDies).GameEventHandler += GameOver;
        NvpEventController.Events(MyEvent.LevelFinish).GameEventHandler += WinLevel;
    }

    private void WinLevel(object sender, EventArgs e) => WinLevel();

    private void GameOver(object sender, EventArgs e)
    {
        EndGame();
        for (int i = 0; i < Enemies.transform.childCount; i++)
        {
            Enemies.transform.GetChild(i).GetComponent<EnemyMovement>().enabled = false;
            Enemies.transform.GetChild(i).GetComponent<Enemy>().enabled = false;
        }
    }

    private void OnDestroy()
    {
        NvpEventController.Events(MyEvent.PlayerDies).GameEventHandler -= GameOver; ;
    }

    #endregion

    public int LevelCount { get => LastLevel - FirstLevel + 1; }

    public Scenes FirstLevel { get => Scenes.Lvl00; }

    public Scenes LastLevel { get => Enum.GetValues(typeof(Scenes)).Cast<Scenes>().Max(); }

    public void LoadLevel(Scenes sceneToLoad) => LoadLevel((int)sceneToLoad);

    public void LoadLevel(int sceneToLoad)
    {
        Time.timeScale = 1f;

        if (scene == LastLevel && sceneToLoad >= SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene((int)Scenes.LevelSelect);
            return;
        }

        scene = (Scenes)sceneToLoad;

        if (scene != Scenes.Null)
        {
            ResetUI();
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    public void LoadNextLevel() => LoadLevel(scene + 1);

    [HideInInspector]
    public Scenes scene = Scenes.Null;

    [HideInInspector]
    public bool gameIsOver = false;
    public GameObject gameOverUI;
    public GameObject completeLevelUI;

    private void Start()
    {
        gameIsOver = false;
    }

    private void ResetUI()
    {
        if (gameOverUI)
            gameOverUI.SetActive(false);
        if (completeLevelUI)
            completeLevelUI.SetActive(false);
    }

    public void EndGame()
    {
        if (gameIsOver)
            return;

        if (gameOverUI)
            gameIsOver = true;
        if (completeLevelUI)
            gameOverUI.SetActive(true);
    }

    public void WinLevel()
    {
        if (gameOverUI)
            gameIsOver = true;
        if (completeLevelUI)
            completeLevelUI.SetActive(true);
    }

    public void ReloadeLevel()
    {
        ResetUI();
        LoadLevel(scene);
    }

    public void LoadMenu() => SceneFader.Instance.FadeToScene(Scenes.Menu);
}
