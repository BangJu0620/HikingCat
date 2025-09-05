using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SocialPlatforms.Impl;


public class FirebaseManager : Singleton<FirebaseManager>
{
    private struct FirebaseConfig
    {
        private string ApiKey;
        private string ProjectId;

        public string UploadURL => $"https://firestore.googleapis.com/v1/projects/{ProjectId}/databases/(default)/documents/scores?key={ApiKey}";
        public string GetURL => $"https://firestore.googleapis.com/v1/projects/{Config.ProjectId}/databases/(default)/documents:runQuery?key={Config.ApiKey}";

        public FirebaseConfig(string apiKey, string projectId)
        {
            ApiKey = apiKey;
            ProjectId = projectId;
        }
    }

    private static readonly FirebaseConfig Config = new FirebaseConfig
        (
            "AIzaSyBq8LHHshETO6cN9EUjWVmQ9dGbLzQ83l4",
            "hikingcat-74ba7"
        );


    public async Task<bool> UploadScore(LeaderboardData data)
    {
        string url = Config.UploadURL;

        string json = $@"{{
            ""fields"": {{
                ""username"": {{""stringValue"": ""{data.name}""}},
                ""time"": {{""doubleValue"": ""{data.time}""}}
            }}
        }}";

        using (UnityWebRequest req = new UnityWebRequest(url, "POST"))
        {
            byte[] rawData = Encoding.UTF8.GetBytes(json);
            req.uploadHandler = new UploadHandlerRaw(rawData);
            req.downloadHandler = new DownloadHandlerBuffer();
            req.SetRequestHeader("Content-Type", "application/json");

            var operation = req.SendWebRequest();

            // ��û �Ϸ� ���� ���
            while (!operation.isDone) await Task.Yield();

            if (req.result == UnityWebRequest.Result.Success)
            {
                // ��� ���� �� ó�� �߰�
                Debug.Log("������ ���� ����");
                return true;
            }
            else
            {
                // ��� ���� �� ó�� �߰�
                Debug.Log("������ ���� ����");
                return false;
            }
        }
    }

    public async Task<List<LeaderboardData>> GetLoaderBoards(int limit)
    {
        string url = Config.GetURL;

        string json = $@"{{
            ""structuredQuery"": {{
                ""from"": [{{ ""collectionId"": ""scores"" }}],
                ""orderBy"": [{{ ""field"": {{ ""fieldPath"": ""time"" }}, ""direction"": ""ASCENDING"" }}],
                ""limit"": {limit}
            }}
        }}";
    
        using (UnityWebRequest req = new UnityWebRequest(url, "POST"))
        {
            byte[] rawData = Encoding.UTF8.GetBytes(json);
            req.uploadHandler = new UploadHandlerRaw(rawData);
            req.downloadHandler = new DownloadHandlerBuffer();
            req.SetRequestHeader("Content-Type", "application/json");

            var operation = req.SendWebRequest();

            // ��û �Ϸ� ���� ���
            while (!operation.isDone) await Task.Yield();

            if (req.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("�ε� ���� : " + req.downloadHandler.text);
                string result = req.downloadHandler.text;

                FirestoreResponse response = JsonUtility.FromJson<FirestoreResponse>(result);
                List<LeaderboardData> datas = new List<LeaderboardData>();

                if (response.documents != null)
                {
                    foreach (var doc in response.documents)
                    {
                        LeaderboardData data = new LeaderboardData
                        {
                            name = doc.fields.name?.stringValue,
                            time = float.TryParse(doc.fields.time?.doubleValue, out var val) ? val : 0f
                        };
                        datas.Add(data);
                    }
                }
                return datas;
            }
            else
            {
                Debug.LogError("���� ����: " + req.error + "\n" + req.downloadHandler.text);
                return null;
            }
        }
    }
}
