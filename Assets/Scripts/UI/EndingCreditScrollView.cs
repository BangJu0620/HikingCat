using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(ScrollRect))]
public class EndingCreditScrollView : MonoBehaviour
{
    [SerializeField, Min(10f)] private float pixelsPerSecond = 100f;
    [SerializeField, Min(1)] private int stableFramesNeeded = 5;
    [SerializeField, Min(0f)] private float stableWaitTimeout = 1.5f;

    private ScrollRect sr;
    private RectTransform content;
    private RectTransform viewport;

    private void Awake()
    {
        sr = GetComponent<ScrollRect>();
        content  = sr.content;
        viewport = sr.viewport;

        sr.inertia = false;
        sr.enabled = false;
    }

    private void OnEnable()
    {
        StartCoroutine(CoRun());
    }

    private IEnumerator CoRun()
    {
        yield return null;

        float lastH = -1f;
        int stable = 0;
        float deadline = Time.unscaledTime + stableWaitTimeout;

        while (true)
        {
            Canvas.ForceUpdateCanvases();
            LayoutRebuilder.ForceRebuildLayoutImmediate(content);

            float h = content.rect.height;
            if (Mathf.Approximately(h, lastH)) stable++;
            else stable = 0;
            lastH = h;

            if (stable >= stableFramesNeeded || Time.unscaledTime > deadline)
                break;

            yield return new WaitForEndOfFrame();
        }

        float travel = Mathf.Max(0f, content.rect.height - viewport.rect.height);
        if (travel <= 0.5f) yield break; 

        Vector2 start = content.anchoredPosition;
        float targetY = travel;
        float remaining = Mathf.Max(0f, targetY - start.y);
        float duration  = remaining / Mathf.Max(1f, pixelsPerSecond);
        float t = 0f;

        while (t < duration)
        {
            t += Time.unscaledDeltaTime;
            float p = Mathf.Clamp01(t / duration);
            float y = Mathf.Lerp(start.y, targetY, p);
            content.anchoredPosition = new Vector2(start.x, y);
            yield return null;
        }

        content.anchoredPosition = new Vector2(start.x, targetY);
    }
}