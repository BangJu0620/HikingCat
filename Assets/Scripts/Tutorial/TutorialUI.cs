using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private Animator[] animators;
    [SerializeField] private string[] Triggers;
    [SerializeField] private float delayTime;

    private void Start()
    {
        StartCoroutine(PlayAnimation());
    }

    private IEnumerator PlayAnimation()
    {
        int i = 0;

        while (true)
        {
            PlayAll(Triggers[i++]);

            yield return new WaitForSecondsRealtime(delayTime);
            i %= Triggers.Length;
        }
    }

    public void PlayAll(string trigger)
    {
        foreach (var anim in animators)
            anim.SetTrigger(trigger);
    }
}
