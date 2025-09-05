using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonClickSound : MonoBehaviour
{
    [SerializeField] private AudioClip click;

    private void Awake()
    {
        var button = GetComponent<Button>();
        if (button)
            button.onClick.AddListener(PlayClickSound);

        if (!click)
            click = Resources.Load<AudioClip>("Audio/MouseClick");
    }

    private void PlayClickSound()
    {
        if (click)
            SoundManager.Instance.PlaySFX(click, 1f);
    }
}
