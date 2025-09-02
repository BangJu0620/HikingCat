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
            //StartCoroutine(Damage());
            InvokeRepeating("TickThornDamage", 0, delay);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log($"{collision.gameObject.name} exit");
        if (collision.gameObject.CompareTag("Player"))
        {
            //StopAllCoroutines();
            CancelInvoke("TickThornDamage");
        }
    }

    //IEnumerator Damage()
    //{
    //    while (true)
    //    {
    //        // ������ ������

    //        Debug.Log("������");

    //        yield return new WaitForSeconds(delay);
    //    }
    //}

    void TickThornDamage()
    {
        // ������ ������

        Debug.Log($"{damage} ������");
    }
}
