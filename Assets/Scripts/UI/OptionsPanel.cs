using UnityEngine;
using UnityEngine.UI;

public class OptionsPanel : MonoBehaviour
{
    [SerializeField] private Button returnButton;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private GameObject bgmMuteIcon;
    [SerializeField] private GameObject sfxMuteIcon;

    private GameObject prev;

    private void Awake()
    {
        if (returnButton) returnButton.onClick.AddListener(OnClickReturn);
        prev = UIManager.Instance.previousPanel;
    }

    private void OnEnable()
    {
        var sm = SoundManager.Instance;
        if (sm)
        {
            bgmSlider.SetValueWithoutNotify(DbToLinear(sm.bgmVolume));
            sfxSlider.SetValueWithoutNotify(DbToLinear(sm.sfxVolume));
        }

        UpdateMuteIcons();

        bgmSlider.onValueChanged.AddListener(OnBgmChanged);
        sfxSlider.onValueChanged.AddListener(OnSfxChanged);
    }

    private void OnDisable()
    {
        bgmSlider.onValueChanged.RemoveListener(OnBgmChanged);
        sfxSlider.onValueChanged.RemoveListener(OnSfxChanged);
    }

    private void OnBgmChanged(float v01)
    {
        var sm = SoundManager.Instance;
        if (sm) sm.SetVolume(v01, SoundManager.VolumeType.Music);
        UpdateMuteIcons();
    }

    private void OnSfxChanged(float v01)
    {
        var sm = SoundManager.Instance;
        if (sm) sm.SetVolume(v01, SoundManager.VolumeType.SFX);
        UpdateMuteIcons();
    }

    private void UpdateMuteIcons()
    {
        if (bgmMuteIcon) bgmMuteIcon.SetActive(bgmSlider.value <= 0.0001f);
        if (sfxMuteIcon) sfxMuteIcon.SetActive(sfxSlider.value <= 0.0001f);
    }

    public void OnClickReturn()
    {
        UIManager.Instance.PlayClickSFX();
        prev = UIManager.Instance.previousPanel;
        UIManager.Instance.Hide(gameObject);
        if (prev) UIManager.Instance.Show(prev);
    }
    
    private static float DbToLinear(float db)
    {
        return db <= -79.9f ? 0f : Mathf.Pow(10f, db / 20f);
    }
}