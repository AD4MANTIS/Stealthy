using UnityEngine;
using UnityEngine.UI;
public class LevelSelector : MonoBehaviour
{
    public Transform levelsMenuContent;
    public UnityEngine.UI.Button levelButtonPrefab;

    private Button[] levelButtons;

    private void Start()
    {
        int levelReached = PlayerPrefs.GetInt("levelReached", 0);
        var levelCount = GameManager.Instance.LevelCount;

        for (int i = 0; i < levelCount; i++)
        {
            var button = Instantiate(levelButtonPrefab, levelsMenuContent);

            int levelIndex = i;

            if (levelIndex <= levelReached)
                button.onClick.AddListener(() => SelectLevel(levelIndex));
            else
                button.interactable = false;


            button.GetComponentInChildren<Text>().text = string.Format("{0:00}", i);
        }

        var resetButton = Instantiate(levelButtonPrefab, levelsMenuContent);
        resetButton.onClick.AddListener(() => PlayerPrefs.SetInt("levelReached", 0));
        var buttonText = resetButton.GetComponentInChildren<Text>();
        buttonText.resizeTextForBestFit = true;
        buttonText.text = "RESET";
    }

    public void SelectLevel(int levelIndex) => SceneFader.Instance.FadeToScene(GameManager.Scenes.Lvl00 + levelIndex);
}
