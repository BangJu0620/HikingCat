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

        // 등록된 키가 있으면 해당 이름 자동 입력
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

        // 등록 시 이름 저장
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
            // 등록 되면 버튼 비활성화
            RegisterButton.onClick.RemoveAllListeners();
            RegisterButton.image.color = Color.gray;
            TMP_Text buttonText = RegisterButton.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = "등록 완료";
        }
        else
        {

        }
        // 완료되면 나가기 알아서? 아니면 주석처리 하면 됨
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
