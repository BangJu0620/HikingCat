using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public IEnumerator OpenDoor(float distance, float speed, int direction = 1, bool isHorizontal = false)   // 
    {
        Vector3 targetPos = transform.position;
        if (isHorizontal)
        {
            targetPos.x += distance * direction;
        }
        else
        {
            targetPos.y += distance * direction;
        }

        while ((targetPos - transform.position).sqrMagnitude > distance)
        {
            transform.position = Vector2.Lerp(transform.position, targetPos, speed * Time.deltaTime);
            yield return null;
        }
    }
}
