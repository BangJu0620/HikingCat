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
        if(collision.gameObject.TryGetComponent(out PlayerController player))
        {
            StartCoroutine(ReSpawnPlayer(player, delay));
        }
    }

    IEnumerator ReSpawnPlayer(PlayerController player, float delayTime)
    {
        player.isLocked = true;
        yield return StartCoroutine(UIManager.Instance.FadeOut());

        player.Spawn();

        yield return new WaitForSeconds(delayTime);

        yield return StartCoroutine(UIManager.Instance.FadeIn());
        player.isLocked = false;
    }
}
