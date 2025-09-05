using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public class SceneLoader : Singleton<SceneLoader>
{
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

        // �ε� �� ������ ���
        while (!asyncOper.isDone)
        {
            yield return null;
        }

        callbackAction?.Invoke();
    }
}