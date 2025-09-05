using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ClearPopupUI : MonoBehaviour
{
    [SerializeField] TMP_Text TimeText;

    [SerializeField] TMP_InputField nameInputField;
    [SerializeField] Button RegisterButton;
    [SerializeField] Button ExitButton;

    LeaderboardData data;

    [SerializeField] GameObject LoadingUIObj;

    private readonly string NameKey = "PlayerName";

    private void Awake()
    {
        UIManager.Instance.clearPopup = this;
        this.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        data = new LeaderboardData();
        data.time = GameManager.Instance.gameTime;
        GameManager.Instance.gameData.gameTime = data.time;
        // 등록된 키가 있으면 해당 이름 자동 입력
        if (PlayerPrefs.HasKey(NameKey))
        {
            data.username = PlayerPrefs.GetString(NameKey);
            nameInputField.text = data.username;
        }
        if (LoadingUIObj != null)
        {
            LoadingUIObj.SetActive(false);
        }

        RegisterButton.onClick.RemoveAllListeners();
        RegisterButton.onClick.AddListener(RegisterClearData);

        ExitButton.onClick.RemoveAllListeners();
        ExitButton.onClick.AddListener(OnClickExitButton);

        nameInputField.onValueChanged.RemoveAllListeners();
        nameInputField.onValueChanged.AddListener(OnChangeInputField);

        int hour = (int)GameManager.Instance.gameTime / 3600;
        int minute = (int)(GameManager.Instance.gameTime % 3600) / 60;
        float second = GameManager.Instance.gameTime % 60;
        TimeText.text = $"소요 시간 : <color=red>{hour.ToString("D2")} : {minute.ToString("D2")} : {second.ToString("00.00")}";
    }

    private void OnDisable()
    {
        data = null;
    }

    private void OnChangeInputField(string text)
    {
        data.username = text;
    }

    private async void RegisterClearData()
    {
        if (data.username == "") return;

        // 등록 시 이름 저장
        PlayerPrefs.SetString(NameKey, data.username);


        if (LoadingUIObj != null)
        {
            LoadingUIObj.SetActive(true);
        }
        bool isRegister = await Leaderboard.RegisterScore(data);


        if (LoadingUIObj != null)
        {
            LoadingUIObj.SetActive(false);
        }

        if (isRegister)
        {
            // 등록 되면 버튼 비활성화
            RegisterButton.onClick.RemoveAllListeners();
            RegisterButton.image.color = Color.gray;
            TMP_Text buttonText = RegisterButton.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = "등록 완료";
        }
    }

    private void OnClickExitButton()
    {
        StartCoroutine(ClickExitButton());
    }

    IEnumerator ClickExitButton()
    {
        yield return StartCoroutine(UIManager.Instance.FadeOut());

        SceneLoader.Instance.LoadEndingScene();
    }

}
