using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{

    public void LoadTitleScene() => SceneManager.LoadScene("TitleScene");
    public void LoadGameScene() =>  SceneManager.LoadScene("GameScene_Test");
    public void QuitGame() => Application.Quit();
}