using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TitlePanel : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private GameObject optionsPanel;

    private PanelController pc;

    private void Awake()
    {
        pc = GetComponent<PanelController>();

        if (startButton)
            startButton.onClick.AddListener(OnClickStart);
        if (quitButton)
            quitButton.onClick.AddListener(OnClickQuit);
        if (optionsButton)
            optionsButton.onClick.AddListener(OnClickOptions);
    }

    public void OnClickStart()
    {
        SceneLoader.Instance.LoadGameScene();
    }

    public void OnClickQuit()
    {
        SceneLoader.Instance.QuitGame();
    }

    public void OnClickOptions()
    {
        UIManager.Instance.previousPanel = gameObject;
        UIManager.Instance.Hide(gameObject);
        UIManager.Instance.Show(optionsPanel);
    }
}
