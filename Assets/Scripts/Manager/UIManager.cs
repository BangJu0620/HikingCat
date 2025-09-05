using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public Fade fade;
    public ClearPopupUI clearPopup;

    public GameObject previousPanel = null;

    public void Show(GameObject panel) => panel?.SetActive(true);
    public void Hide(GameObject panel) => panel?.SetActive(false);

    public IEnumerator FadeIn()
    {
        yield return StartCoroutine(fade.FadeIn());
    }

    public IEnumerator FadeOut()
    {
        yield return StartCoroutine(fade.FadeOut());
    }

    public void SetClear()
    {
        clearPopup.gameObject.SetActive(true);
    }
}
