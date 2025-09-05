using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearZone : MonoBehaviour
{
    [SerializeField] float delayTime;

    bool isCleared;

    void SceneLoad()
    {
        // 페이드아웃
        // 페이드 화면의 알파값을 조절해서 점점 안 보이게
        // 아니면 원 형태로 검은색 배경이 점점 모여서 꽉 차게?
        
        // 페이드아웃 끝나면 씬 이동
        Debug.Log("씬 이동");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isCleared)
        {
            isCleared = true;

            // 팝업 띄우기
            Debug.Log("클리어");
            UIManager.Instance.SetClear();

            // 씬 이동하기
            Invoke("SceneLoad", delayTime);
        }
    }
}
