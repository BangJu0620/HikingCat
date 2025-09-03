using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : Singleton<SoundManager>
{
    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private string masterVolumeParam = "MasterVolume";
    [SerializeField] private string bgmVolumeParam = "BGMVolume";
    [SerializeField] private string sfxVolumeParam = "SFXVolume";

    [Header("Audio Sources")]
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private GameObject sfxSourcePrefab;


    Queue<AudioSource> sfxQueue = new();
    [SerializeField] private int sfxPoolCount = 10;

    private const string ResourcePath = "Audio/";
    private readonly string MixerPath = string.Concat(ResourcePath,"AudioMixer");
    [SerializeField] private string BGMPrefPath = string.Concat(ResourcePath, "BGM Audiosource");
    private readonly string SFXPrefPath = string.Concat(ResourcePath, "SFX Audiosource");

    public enum VolumeType
    {
        Master, Music, SFX
    }

    private float masterVolume, bgmVolume, sfxVolume;
    private float SetMasterVolume(float volume) => masterVolume = Mathf.Log10(Mathf.Clamp01(volume)) * 20f;
    private float SetBGMVolume(float volume) => bgmVolume = Mathf.Log10(Mathf.Clamp01(volume)) * 20f;
    private float SetSFXVolume(float volume) => sfxVolume = Mathf.Log10(Mathf.Clamp01(volume)) * 20f;


    protected override void Initialize()
    {
        base.Initialize();
        audioMixer = Resources.Load<AudioMixer>(MixerPath);
    }

    public void SetVolume(float volume, VolumeType type = VolumeType.Master)
    {
        switch (type)
        {
            case VolumeType.Master:
                SetMasterVolume(volume);
                break;
            case VolumeType.Music:
                SetBGMVolume(volume);
                break;
            case VolumeType.SFX:
                SetSFXVolume(volume);
                break;
        }

        audioMixer.SetFloat(masterVolumeParam, masterVolume);
        audioMixer.SetFloat(bgmVolumeParam, bgmVolume);
        audioMixer.SetFloat(sfxVolumeParam, sfxVolume);

    }

    #region BGM

    public void PlayBGM(AudioClip newClip, bool loop = true, float fadeDuration = 1f)
    {
        if (bgmSource == null)
        {
            var a = Resources.Load<GameObject>(BGMPrefPath);
            bgmSource = Instantiate(a, transform).GetComponent<AudioSource>();
        }

        if (bgmSource.clip == newClip) return;

        StopAllCoroutines(); // 기존 페이드 중지
        StartCoroutine(FadeAndSwitchBGM(newClip, loop, fadeDuration));
    }

    private IEnumerator FadeAndSwitchBGM(AudioClip newClip, bool loop, float fadeDuration)
    {
        // 페이드아웃
        float currentVolume;
        audioMixer.GetFloat(bgmVolumeParam, out currentVolume);
        currentVolume = Mathf.Pow(10, currentVolume / 20f); // dB → linear

        for (float t = 0; t < fadeDuration; t += Time.unscaledDeltaTime)
        {
            float v = Mathf.Lerp(currentVolume, 0f, t / fadeDuration);
            audioMixer.SetFloat(bgmVolumeParam, Mathf.Log10(Mathf.Max(v, 0.0001f)) * 20f);
            yield return null;
        }

        audioMixer.SetFloat(bgmVolumeParam, -80f); // 완전 음소거

        // 클립 교체
        bgmSource.Stop();
        bgmSource.clip = newClip;
        bgmSource.loop = loop;
        bgmSource.Play();

        // 페이드인
        float targetDesbel = Mathf.Pow(10, bgmVolume / 20);
        for (float t = 0; t < fadeDuration; t += Time.unscaledDeltaTime)
        {
            float v = Mathf.Lerp(0f, targetDesbel, t / fadeDuration);
            audioMixer.SetFloat(bgmVolumeParam, Mathf.Log10(Mathf.Max(v, 0.0001f)) * 20f);
            yield return null;
        }

        audioMixer.SetFloat(bgmVolumeParam, bgmVolume); // 0 dB로 고정
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }


    #endregion

    #region SFX

    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        AudioSource source = GetAvailableSFXSource();
        source.clip = clip;
        source.volume = volume;
        source.Play();
        StartCoroutine(DisableWhenDone(source));
    }


    private AudioSource GetAvailableSFXSource()
    {
        if(sfxQueue.Count > 0)
        {
            var ret = sfxQueue.Dequeue();
            ret.gameObject.SetActive(true);
            return ret;
        }

        if (sfxSourcePrefab == null)
        {
            sfxSourcePrefab = Resources.Load<GameObject>(SFXPrefPath);
        }
        var newSource = Instantiate(sfxSourcePrefab, transform);
        return newSource.GetComponent<AudioSource>();
    }

    private IEnumerator DisableWhenDone(AudioSource src)
    {
        yield return new WaitUntil(() => !src.isPlaying);

        src.clip = null;

        if(sfxQueue.Count <= sfxPoolCount)
        {
            sfxQueue.Enqueue(src);
            src.gameObject.SetActive(false);
        }
        else
        { 
            Destroy(src.gameObject);
        }
    }


    #endregion
}
