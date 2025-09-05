using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearZone : MonoBehaviour
{
    [SerializeField] float delayTime;

    bool isCleared;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isCleared)
        {
            isCleared = true;

            // ÆË¾÷ ¶ç¿ì±â
            UIManager.Instance.SetClear();
        }
    }
}
