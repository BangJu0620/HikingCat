using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingBGM : MonoBehaviour
{
    private const string BgmPath = "Sounds/Ending_BGM";
    [SerializeField] private float fadeDuration = 1f;

    private void Awake()
    {
        var bgm = Resources.Load<AudioClip>(BgmPath);
        SoundManager.Instance.PlayBGM(bgm, true, fadeDuration);
    }
    
}
