using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameSceneUIRoot : MonoBehaviour
{
    [SerializeField] private Button pauseButton;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject pausePanel;

    private void Awake()
    {
        if (pauseButton)
            pauseButton.onClick.AddListener(OnClickPause);
    }

    public void OnClickPause()
    {
        if (!UIManager.Instance.isFadeComplete) return;
        Debug.Log("Test");
        UIManager.Instance.PlayClickSFX();
        if (pausePanel.activeSelf)
        {
            UIManager.Instance.Hide(pausePanel);
            GameManager.Instance.ResumeGame();
        }
        else
        {
            UIManager.Instance.Show(pausePanel);
            GameManager.Instance.PauseGame();
        }  
    }

    public void OnClickEscape(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            OnClickPause();
        }
    }
}
