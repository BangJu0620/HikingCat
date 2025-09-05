using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    [SerializeField] protected bool dontDestroyOnLoad = true;

    // �� �÷��װ� ��� �̱����� ���� ���¸� �����մϴ�.
    public static bool IsApplicationQuit { get; private set; } = false;
    public static bool IsDestroying { get; private set; } = false;
    public static T Instance
    {
        get
        {
            if (IsApplicationQuit || IsDestroying)
                return null;

            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    GameObject singletonObject = new(typeof(T).Name);
                    instance = singletonObject.AddComponent<T>();
                }
            }
            return instance;
        }
    }

    protected virtual void OnDestroy()
    {
        IsDestroying = true; // �� �����ӿ��� ���� Instance ���� ����
        if (instance == this) instance = null;
        IsDestroying = false;
    }

    protected virtual void Awake()
    {
        // ���� ���� ��, �÷��׸� �ʱ�ȭ
        IsApplicationQuit = false;

        if (instance == null)
        {
            Initialize();
            if (dontDestroyOnLoad)
                DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    protected virtual void Initialize() { }

    private void OnApplicationQuit()
    {
        // ���ø����̼� ���� �÷��׸� true�� ����
        IsApplicationQuit = true;
    }

    public void Release()
    {
        instance = null;
        Destroy(gameObject);
    }

}