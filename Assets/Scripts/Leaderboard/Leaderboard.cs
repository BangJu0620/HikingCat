using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class LeaderboardData
{
    public string username;
    public float time;
    public string timeText => TimeToText();

    private string TimeToText()
    {
        // �Ҽ��� �� �ڸ����� �����ؼ� ��ȯ
        int totalSeconds = (int)time;
        int hours = totalSeconds / 3600;
        int minutes = (totalSeconds % 3600) / 60;
        int seconds = totalSeconds % 60;
        int centiseconds = (int)((time - totalSeconds) * 100); // �Ҽ��� �� �ڸ�

        // �ð��� �ּ� 2�ڸ�, 3�ڸ� �̻��̸� �ڵ� Ȯ��
        string hourFormat = hours >= 100 ? "000" : "00";

        return string.Format("{0:" + hourFormat + "}:{1:00}:{2:00}.{3:00}",
                             hours, minutes, seconds, centiseconds);
    }

    public LeaderboardData() { }

    public LeaderboardData(string name, float time)
    {
        this.username = name;
        this.time = time;
    }
}

[Serializable]
public class FirestoreValue
{
    public string stringValue;
    public string doubleValue;
    public string integerValue;
}

[Serializable]
public class FirestoreFields
{
    public FirestoreValue username;
    public FirestoreValue time;
}

[Serializable]
public class FirestoreDocument
{
    public string name;
    public FirestoreFields fields;
}

[Serializable]
public class FirestoreDocumentWrapper
{
    public FirestoreDocument document;
}


public static class Leaderboard
{
    public static LeaderboardData CreateNewScore(string name, float time)
    {
        LeaderboardData outData = new LeaderboardData(name, time);
        return outData;
    }

    public static async Task<List<LeaderboardData>> LoadLeaderboards(int limit)
    {
        return await FirebaseManager.Instance.GetLoaderBoards(limit);

    }

    public static async Task<bool> RegisterScore(LeaderboardData data)
    {
        return await FirebaseManager.Instance.UploadScore(data);

    }
}