using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ThornyBush : MonoBehaviour
{
    [SerializeField] float delay;
    [SerializeField] int damage;

    [SerializeField] Fade fade;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"{collision.gameObject.name} enter");

        if(collision.gameObject.TryGetComponent(out PlayerController player))
        {
            StartCoroutine(ReSpawnPlayer(player, delay));
        }
    }

    IEnumerator ReSpawnPlayer(PlayerController player, float delayTime)
    {
        yield return StartCoroutine(fade.FadeOut());

        player.Spawn();

        yield return new WaitForSeconds(delayTime);

        yield return StartCoroutine(fade.FadeIn());
    }
}
