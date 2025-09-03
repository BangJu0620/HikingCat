using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsPanel : MonoBehaviour
{
    [SerializeField] private Button returnButton;
    [SerializeField] private Slider BGM_Slider;
    [SerializeField] private Slider SFX_Slider;
    [SerializeField] private AudioMixer mixer;
    private string bgmParameter = "Test_BGM";
    private string sfxParameter = "Test_SFX";
    [SerializeField] private GameObject bgmToggle;
    [SerializeField] private GameObject sfxToggle;
    private GameObject prev = null;

    private PanelController pc;

    private void Awake()
    {
        pc = GetComponent<PanelController>();

        if (returnButton)
            returnButton.onClick.AddListener(OnClickReturn);

        prev = UIManager.Instance.previousPanel;
    }

    private void Start()
    {
        ApplyBGMVolume(BGM_Slider.value);
        ApplySFXVolume(SFX_Slider.value);

        BGM_Slider.onValueChanged.AddListener(ApplyBGMVolume);
        SFX_Slider.onValueChanged.AddListener(ApplySFXVolume);
    }

    public void OnClickReturn()
    {
        prev = UIManager.Instance.previousPanel;
        UIManager.Instance.Hide(gameObject);

        if (prev)
            UIManager.Instance.Show(prev);
    }

    private void OnDestroy()
    {
        if (BGM_Slider)
            BGM_Slider.onValueChanged.RemoveListener(ApplyBGMVolume);
        if (SFX_Slider)
            SFX_Slider.onValueChanged.RemoveListener(ApplySFXVolume);
    }

    public void ApplyBGMVolume(float value)
    {
        float dB = (value <= 0.0001f) ? -80f : Mathf.Log10(value) * 20f;
        mixer.SetFloat(bgmParameter, dB);

        if (bgmToggle) bgmToggle.SetActive(value <= 0.0001f);
    }

    public void ApplySFXVolume(float value)
    {
        float dB = (value <= 0.0001f) ? -80f : Mathf.Log10(value) * 20f;
        mixer.SetFloat(sfxParameter, dB);

        if (sfxToggle) sfxToggle.SetActive(value <= 0.0001f);
    }
}
