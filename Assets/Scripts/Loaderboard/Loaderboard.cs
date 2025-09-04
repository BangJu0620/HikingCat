using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public struct LeaderboardData
{
    public string name;
    public float time;

    public LeaderboardData(string name, float time)
    {
        this.name = name;
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
    public FirestoreValue name;
    public FirestoreValue time;
}

[Serializable]
public class FirestoreDocument
{
    public string name;
    public FirestoreFields fields;
}

[Serializable]
public class FirestoreResponse
{
    public FirestoreDocument[] documents;
}

public static class Loaderboard
{
    public static void CreateNewScore(string name, float time, out LeaderboardData outData)
    {
        outData.name = name;
        outData.time = time;
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