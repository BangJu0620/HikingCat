using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    [SerializeField] float fadeSpeed;

    Image panel;

    private void Awake()
    {
        panel = GetComponent<Image>();
        StartCoroutine(FadeOutIn(2));
    }

    public IEnumerator FadeIn()
    {
        while(panel.color.a > 0)
        {
            //Debug.Log(panel.color.a);
            panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, Mathf.MoveTowards(panel.color.a, 0, Time.deltaTime * fadeSpeed));
            yield return null;
        }
        //panel.gameObject.SetActive(false);
    }

    public IEnumerator FadeOut()
    {
        //panel.gameObject.SetActive(true);
        while (panel.color.a < 1)
        {
            //Debug.Log(panel.color.a);
            panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, Mathf.MoveTowards(panel.color.a, 1, Time.deltaTime * fadeSpeed));
            yield return null;
        }
    }

    public IEnumerator FadeOutIn(float delayTime)
    {
        yield return StartCoroutine(FadeOut());

        Test();

        yield return new WaitForSeconds(delayTime);

        yield return StartCoroutine(FadeIn());
    }

    void Test()
    {
        Debug.Log("test");
    }
}
