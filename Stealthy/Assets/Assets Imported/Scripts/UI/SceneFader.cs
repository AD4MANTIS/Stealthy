using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFader : MonoBehaviour
{
    #region Singleton

    public static SceneFader Instance { get; private set; }

    private void Awake()
    {
        if (Instance)
            Debug.LogError("More than one Singleton Object");
        else
            Instance = this;
        StartCoroutine(FadeIn());
    }

    #endregion

    public Image image;
    public AnimationCurve alphaCurve;

    public void FadeToScene(GameManager.Scenes scene) => FadeToScene((int)scene);

    public void FadeToScene(int buidIndex)
    {
        StartCoroutine(FadeOut());
        StartCoroutine(LoadLevel(buidIndex));
    }

    public void FadeToLevel(GameManager.Scenes level)
    {
        StartCoroutine(FadeOut());
        StartCoroutine(LoadLevel((int)level));
    }

    private IEnumerator LoadLevel(int level)
    {
        yield return new WaitForSecondsRealtime(7f);
        GameManager.Instance.LoadLevel(level);
        StartCoroutine(FadeIn());
    }

    private IEnumerator StartFade()
    {
        yield return FadeOut();
        yield return FadeIn();
    }

    private IEnumerator FadeIn()
    {
        float time = 1f;

        if (image)
            while (time > 0f)
            {
                time -= Time.deltaTime;
                image.color = new Color(0f, 0f, 0f, alphaCurve.Evaluate(time));
                yield return 0;
            }
    }

    private IEnumerator FadeOut()
    {
        float time = 0f;

        if (image)
            while (time < 1f)
            {
                time += Time.deltaTime;
                image.color = new Color(0f, 0f, 0f, alphaCurve.Evaluate(time));
                yield return 0;
            }
    }
}
