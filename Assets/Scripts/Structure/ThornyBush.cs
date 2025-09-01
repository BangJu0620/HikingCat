using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ThornyBush : MonoBehaviour
{
    [SerializeField] float delay;
    [SerializeField] float damage;

    // 플레이어 체력 어케 할건지 좀 봐야될듯

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"{collision.gameObject.name} enter");
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Damage());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log($"{collision.gameObject.name} exit");
        if (collision.gameObject.CompareTag("Player"))
        {
            StopAllCoroutines();
        }
    }

    IEnumerator Damage()
    {
        while (true)
        {
            // 데미지 입히기

            // 애니메이션도 작동해야될까

            Debug.Log("데미지");

            yield return new WaitForSeconds(delay);
        }
    }
}
