using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int maxValue;
    public int curValue;

    Player player;

    // 임시, 나중에 알맞은 위치에 옮겨야함
    [SerializeField] Transform spawnPos;

    private void Awake()
    {
        curValue = maxValue;
        player = GetComponent<Player>();
    }

    public void Heal(int amount)
    {
        curValue = Mathf.Min(maxValue, curValue + amount);
    }

    public void TakeDamage(int amount, float time)
    {
        Debug.Log($"{amount} 데미지");
        //player.status.CurVelocity = Vector2.zero;
        StartCoroutine(Freeze(time));
        Debug.Log(player.status.CurVelocity);

        curValue = Mathf.Max(0, curValue - amount);
        if(curValue <= 0)
        {
            player.transform.GetComponentInChildren<SpriteRenderer>().color = Color.white;
            // 캐릭터 사망
            Debug.Log("죽음");

            // 위치 이동
            Spawn();
            Heal(maxValue);
        }
    }

    IEnumerator Freeze(float time)
    {
        Debug.Log("Freeze");
        player.status.CurVelocity *= 0.01f;
        player.transform.GetComponentInChildren<SpriteRenderer>().color = Color.red;
        player.status.isLocked = true;
        yield return new WaitForSeconds(time);
        player.transform.GetComponentInChildren<SpriteRenderer>().color = Color.white;
        player.status.isLocked = false;
    }

    // 임시, 나중에 알맞은 위치에 옮겨야함
    void Spawn()
    {
        player.status.CurVelocity = Vector2.down;
        Debug.Log(player.status.CurVelocity = Vector2.down);
        gameObject.transform.position = spawnPos.position;
    }
}
