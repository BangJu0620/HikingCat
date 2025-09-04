using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    [SerializeField] private Button restartButton;

    private void Awake()
    {
        if (restartButton)
            restartButton.onClick.AddListener(() => SceneLoader.Instance.LoadTitleScene());
    }
}
