using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] float distance;
    [SerializeField] float speed;
    [SerializeField] bool isVertical;
    public IEnumerator OpenDoor()
    {
        Vector3 targetPos = transform.position;
        if (isVertical)
        {
            targetPos.x += distance;
        }
        else
        {
            targetPos.y += distance;
        }

        while ((targetPos - transform.position).sqrMagnitude > distance)
        {
            Debug.Log(transform.position);
            transform.position = Vector2.Lerp(transform.position, targetPos, speed * Time.deltaTime);
            yield return null;
        }
    }
}
