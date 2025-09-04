using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingPanel : MonoBehaviour
{
    [SerializeField] private Button returnButton;
    [SerializeField] private GameObject titlePanel;

    private void Awake()
    {
        if (returnButton)
            returnButton.onClick.AddListener(OnClickReturnButton);
    }

    private void OnClickReturnButton()
    {
        UIManager.Instance.Hide(gameObject);
        UIManager.Instance.Show(titlePanel);
    }
}
