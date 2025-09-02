using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    // public static UIManager instance
    // {
    //     get
    //     {
    //         if (Instance == null)
    //         {
    //             Instance = FindObjectOfType<UIManager>();

    //             if (Instance == null)
    //             {
    //                 GameObject go = new GameObject("UIManager");
    //                 Instance = go.AddComponent<UIManager>();
    //             }
    //         }
    //         return Instance;
    //     }
    // }

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
