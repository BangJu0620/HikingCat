using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SceneLoader : Singleton<SceneLoader>
{
    public void LoadTitleScene() => SceneManager.LoadScene("TitleScene");
    public void LoadGameScene() => SceneManager.LoadScene("GameScene_Test");
    public void LoadEndingScene() => SceneManager.LoadScene("EndingScene");
    public void QuitGame() => Application.Quit();
}