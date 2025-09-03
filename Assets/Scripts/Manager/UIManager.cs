using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{

    public GameObject previousPanel = null;

    public void Show(GameObject panel) => panel?.SetActive(true);
    public void Hide(GameObject panel) => panel?.SetActive(false);
}
