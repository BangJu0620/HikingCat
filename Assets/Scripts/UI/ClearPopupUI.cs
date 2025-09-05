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
        // ��ϵ� Ű�� ������ �ش� �̸� �ڵ� �Է�
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
        TimeText.text = $"�ҿ� �ð� : <color=red>{hour.ToString("D2")} : {minute.ToString("D2")} : {second.ToString("00.00")}";
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

        // ��� �� �̸� ����
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
            // ��� �Ǹ� ��ư ��Ȱ��ȭ
            RegisterButton.onClick.RemoveAllListeners();
            RegisterButton.image.color = Color.gray;
            TMP_Text buttonText = RegisterButton.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = "��� �Ϸ�";
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
