using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SceneLoader : Singleton<SceneLoader>
{
    // public event Action<Scene> OnSceneLoaded;
    // public string CurrentSceneName => SceneManager.GetActiveScene().name;
    // public bool IsEndingScene() => CurrentSceneName == "EndingScene";

    // protected override void Initialize()
    // {
    //     base.Initialize();
    //     DontDestroyOnLoad(gameObject);
    //     SceneManager.sceneLoaded += HandleSceneLoaded;
    // }

    // private void OnDestroy() => SceneManager.sceneLoaded -= HandleSceneLoaded;

    // private void HandleSceneLoaded(Scene scene, LoadSceneMode mode) => OnSceneLoaded?.Invoke(scene);

    public void LoadTitleScene() => SceneManager.LoadScene("TitleScene");
    public void LoadGameScene() => SceneManager.LoadScene("GameScene_Test");
    public void LoadEndingScene() => SceneManager.LoadScene("EndingScene");
    public void QuitGame() => Application.Quit();
}