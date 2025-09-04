using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneSoundController : MonoBehaviour
{
    [SerializeField] private AudioClip bgm;

    private void Start()
    {
        SoundManager.Instance.PlayBGM(bgm, loop: true);
    }
}
