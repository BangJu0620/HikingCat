using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private GameObject TitlePanel;
    [SerializeField] private GameObject PausePanel;
    [SerializeField] private GameObject OptionsPanel;
    public GameObject previousPanel = null;

    public GameObject titlePanel => TitlePanel;
    public GameObject pausePanel => PausePanel;
    public GameObject optionsPanel => OptionsPanel;

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
}
