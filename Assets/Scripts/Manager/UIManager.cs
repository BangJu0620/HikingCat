using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();

                if (instance == null)
                {
                    GameObject go = new GameObject("UIManager");
                    instance = go.AddComponent<UIManager>();
                    DontDestroyOnLoad(go);
                }
            }
            return instance;
        }
    }

    private Transform uiRoot;
    public GameObject previousPanel = null;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        CreateUIRoot();
    }

    private void CreateUIRoot()
    {
        if (uiRoot == null)
        {
            var prefab = Resources.Load<GameObject>("Prefabs/UI/UIRoot");
            var root = Instantiate(prefab);
            uiRoot = root.transform.Find("Canvas");
        }
    }

    public void Show(GameObject panel) => panel?.SetActive(true);
    public void Hide(GameObject panel) => panel?.SetActive(false);
}
