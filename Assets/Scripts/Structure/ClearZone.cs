using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearZone : MonoBehaviour
{
    [SerializeField] float delayTime;

    bool isCleared;

    void SceneLoad()
    {
        // ���̵�ƿ�
        // ���̵� ȭ���� ���İ��� �����ؼ� ���� �� ���̰�
        // �ƴϸ� �� ���·� ������ ����� ���� �𿩼� �� ����?
        
        // ���̵�ƿ� ������ �� �̵�
        Debug.Log("�� �̵�");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isCleared)
        {
            isCleared = true;

            // �˾� ����
            Debug.Log("Ŭ����");
            UIManager.Instance.SetClear();

            // �� �̵��ϱ�
            Invoke("SceneLoad", delayTime);
        }
    }
}
