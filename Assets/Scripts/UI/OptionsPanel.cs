using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsPanel : MonoBehaviour
{
    [SerializeField] private Button returnButton;
    [SerializeField] private Slider BGM_Slider;
    [SerializeField] private Slider soundEffect_Slider;
    private GameObject prev = null;

    private PanelController pc;

    private void Awake()
    {
        pc = GetComponent<PanelController>();

        if (returnButton)
            returnButton.onClick.AddListener(OnClickReturn);
        
        prev = UIManager.Instance.previousPanel;
    }

    public void OnClickReturn()
    {
        pc.Hide(gameObject);
        pc.Show(prev);
    }
}
