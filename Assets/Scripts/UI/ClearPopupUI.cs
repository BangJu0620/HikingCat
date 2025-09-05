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


    private void OnEnable()
    {
        data = new LeaderboardData();
        data.time = GameManager.Instance.gameTime;

        // ��ϵ� Ű�� ������ �ش� �̸� �ڵ� �Է�
        if (PlayerPrefs.HasKey(NameKey))
        {
            data.name = PlayerPrefs.GetString(NameKey);
            nameInputField.text = data.name;
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

    }

    private void OnDisable()
    {
        data = null;
    }

    private void OnChangeInputField(string text)
    {
        data.name = text;
    }

    private async void RegisterClearData()
    {
        if (data.name == "") return;

        // ��� �� �̸� ����
        PlayerPrefs.SetString(NameKey, data.name);


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
        else
        {

        }
        // �Ϸ�Ǹ� ������ �˾Ƽ�? �ƴϸ� �ּ�ó�� �ϸ� ��
        ExitButton.onClick.Invoke();
    }

    private void OnClickExitButton()
    {
        StartCoroutine(ClickExitButton());
//        SceneLoader.Instance.LoadEndScrollScene();
    }

    IEnumerator ClickExitButton()
    {
        yield return StartCoroutine(UIManager.Instance.FadeOut());

        // SceneLoader.Instance.load
    }
}
