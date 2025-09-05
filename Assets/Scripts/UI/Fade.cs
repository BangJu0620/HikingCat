using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    [SerializeField] float fadeSpeed;

    Canvas canvas;
    Image panel;

    private void Awake()
    {
        UIManager.Instance.fade = this;
        panel = GetComponent<Image>();
        canvas = GetComponent<Canvas>();
        panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, 1);
        StartCoroutine(FadeIn());
    }

    public IEnumerator FadeIn()
    {
        UIManager.Instance.isFadeComplete = false;
        while(panel.color.a > 0)
        {
            panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, Mathf.MoveTowards(panel.color.a, 0, Time.deltaTime * fadeSpeed));
            yield return null;
        }
        canvas.sortingOrder = 0;
        panel.raycastTarget = false;
        UIManager.Instance.isFadeComplete = true;
    }

    public IEnumerator FadeOut()
    {
        UIManager.Instance.isFadeComplete = false;
        panel.raycastTarget = false;
        canvas.sortingOrder = 200;
        while (panel.color.a < 1)
        {
            panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, Mathf.MoveTowards(panel.color.a, 1, Time.deltaTime * fadeSpeed));
            yield return null;
        }
        UIManager.Instance.isFadeComplete = true;
    }
}
