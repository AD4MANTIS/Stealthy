using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    //#region Singleton

    //public static LevelManager Instance { get; private set; }

    //private void Awake()
    //{
    //    if (Instance)
    //        Debug.LogError("More than one Singleton Object");
    //    else
    //    {
    //        Instance = this;
    //        if (!GameManager.Instance)
    //            SceneManager.LoadScene((int)GameManager.Scenes.ParentLevel, LoadSceneMode.Additive);
    //    }
    //}

    //#endregion

    //public bool ParentSceneLoaded => Instance;
}
