using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

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

    public void LoadTitleScene()
    {
        StartCoroutine(SceneLoad("TitleScene", GameManager.Instance.ResumeGame));
    }

    public void LoadGameScene() 
    {
        StartCoroutine(SceneLoad("InGameScene", GameManager.Instance.GameStart));
    }

    public void LoadEndingScene() => SceneManager.LoadScene("EndingScene");
    public void QuitGame() => Application.Quit();

    private IEnumerator SceneLoad(string sceneName, Action callbackAction = null)
    {
        var asyncOper = SceneManager.LoadSceneAsync(sceneName);

        // 로드 될 때까지 대기
        while (!asyncOper.isDone)
        {
            yield return null;
        }

        callbackAction?.Invoke();
    }
}