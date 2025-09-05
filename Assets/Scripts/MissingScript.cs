using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;

public class MissingScript
{
    [MenuItem("Tools/Clean Missing Scripts In Scene")]
    static void CleanScene()
    {
        foreach (GameObject go in Object.FindObjectsOfType<GameObject>())
        {
            GameObjectUtility.RemoveMonoBehavioursWithMissingScript(go);

            Debug.Log($"{go} Clean");
        }
        Debug.Log("Cleaned missing scripts in scene.");
    }

    [MenuItem("Tools/Clean Missing Scripts In Project")]
    static void CleanProject()
    {
        string[] guids = AssetDatabase.FindAssets("t:Prefab");
        int count = 0;
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            count += GameObjectUtility.RemoveMonoBehavioursWithMissingScript(prefab);
        }
        Debug.Log($"Cleaned missing scripts in {count} prefabs.");
    }
}