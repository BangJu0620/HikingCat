using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button returnToGameButton;
    [SerializeField] private Button returnToTitleButton;
    [SerializeField] private GameObject optionsPanel;

    private PanelController pc;

    private void Awake()
    {
        pc = GetComponent<PanelController>();

        if (optionsButton)
            optionsButton.onClick.AddListener(OnClickOptions);
        if (returnToGameButton)
            returnToGameButton.onClick.AddListener(OnClickReturnToGame);
        if (returnToTitleButton)
            returnToTitleButton.onClick.AddListener(OnClickReturnToTitle);
    }

    public void OnClickOptions()
    {
        UIManager.Instance.PlayClickSFX();
        UIManager.Instance.previousPanel = gameObject;
        UIManager.Instance.Hide(gameObject);
        UIManager.Instance.Show(optionsPanel);
    }

    public void OnClickReturnToGame()
    {
        UIManager.Instance.PlayClickSFX();
        UIManager.Instance.Hide(gameObject);
        GameManager.Instance.ResumeGame();
    }

    public void OnClickReturnToTitle()
    {
        UIManager.Instance.PlayClickSFX();
        SceneLoader.Instance.LoadTitleScene();
    }
}
