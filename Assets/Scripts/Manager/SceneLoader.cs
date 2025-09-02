using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LoadTitleScene()
    {
        Debug.Log("Title Scene.");
        //SceneManager.LoadScene("TitleScene_Test");
    }

    public void LoadGameScene()
    {
        Debug.Log("Game Scene.");
        //SceneManager.LoadScene("GameScene_Test");
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game.");
        Application.Quit();
    }
}
