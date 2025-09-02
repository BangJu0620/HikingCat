using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ThornyBush : MonoBehaviour
{
    [SerializeField] float delay;
    [SerializeField] float damage;

    // �÷��̾� ü�� ���� �Ұ��� �� ���ߵɵ�

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
            // ������ ������

            // �ִϸ��̼ǵ� �۵��ؾߵɱ�

            Debug.Log("������");

            yield return new WaitForSeconds(delay);
        }
    }
}
