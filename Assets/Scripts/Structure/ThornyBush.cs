using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ThornyBush : MonoBehaviour
{
    [SerializeField] float delay;
    [SerializeField] int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"{collision.gameObject.name} enter");
        //if (collision.gameObject.CompareTag("Player"))
        //{
        //    //StartCoroutine(Damage());
        //    InvokeRepeating("TickThornDamage", 0, delay);
        //}
        if(collision.gameObject.TryGetComponent(out PlayerController player))
        {
            player.Spawn();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log($"{collision.gameObject.name} exit");
        //if (collision.gameObject.CompareTag("Player"))
        //{
        //    //StopAllCoroutines();
        //    CancelInvoke("TickThornDamage");
        //}
    }

    //IEnumerator Damage()
    //{
    //    while (true)
    //    {
    //        // 데미지 입히기

    //        Debug.Log("데미지");

    //        yield return new WaitForSeconds(delay);
    //    }
    //}

    //void TickThornDamage()
    //{
    //    // 데미지 입히기

    //    Debug.Log($"{damage} 데미지");
    //}
}
