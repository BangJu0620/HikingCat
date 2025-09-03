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
            Debug.Log(targetPos.x);
        }
        else
        {
            targetPos.y += distance * direction;
            Debug.Log(targetPos.y);
        }

        while ((targetPos - transform.position).magnitude > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            yield return null;
        }
    }
}
