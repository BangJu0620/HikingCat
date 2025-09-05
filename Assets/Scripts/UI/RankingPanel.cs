using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingPanel : MonoBehaviour
{
    [SerializeField] private Button returnButton;
    [SerializeField] private GameObject titlePanel;
    [SerializeField] private Transform content;
    [SerializeField] private GameObject entry;

    private void Awake()
    {
        if (returnButton)
            returnButton.onClick.AddListener(OnClickReturnButton);
    }

    private void OnEnable() => LoadAndDisplay();
    
    public async void LoadAndDisplay()
    {
        foreach (Transform e in content)
            Destroy(e.gameObject);

        var task = Leaderboard.LoadLeaderboards(10);

        // (선택) Loading Ui 

        List<LeaderboardData> list = await task;

        if (list.Count <= 0)
            Debug.Log("No Data.");

        for (int i = 0; i < list.Count; i++)
        {
            LeaderboardData data = list[i];
            var entryData = Instantiate(entry, content);
            var text = entryData.GetComponent<Text>();
            text.text = $"{data.name} - {data.timeText}";
        }
    }

    private void OnClickReturnButton()
    {
        UIManager.Instance.Hide(gameObject);
        UIManager.Instance.Show(titlePanel);
    }
}
