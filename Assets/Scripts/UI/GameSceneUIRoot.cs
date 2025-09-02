using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    private void OnClickPause()
    {
        UIManager.Instance.Show(pausePanel);
        //GameManager.Instance.PauseGame();
    }
}
