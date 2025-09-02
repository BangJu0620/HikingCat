using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button returnToGameButton;
    [SerializeField] private Button returnToTitleButton;

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
        UIManager.Instance.previousPanel = UIManager.Instance.pausePanel;
        pc.Hide(gameObject);
        pc.Show(UIManager.Instance.optionsPanel);
    }

    public void OnClickReturnToGame()
    {
        SceneLoader.Instance.LoadGameScene();
    }

    public void OnClickReturnToTitle()
    {
        SceneLoader.Instance.LoadTitleScene();
    }
}
