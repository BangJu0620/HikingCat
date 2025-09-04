using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] GameObject rightUpDoorObj;
    [SerializeField] GameObject leftDownDoorObj;
    [SerializeField] bool isHorizontal;
    [SerializeField] float distance;
    [SerializeField] float speed;
    [SerializeField] AudioClip doorSFX;
    [SerializeField] float doorVolume;

    public void Open()
    {
        StartCoroutine(OpenDoor(rightUpDoorObj, distance, speed, 1, isHorizontal));
        StartCoroutine(OpenDoor(leftDownDoorObj, distance, speed, -1, isHorizontal));
    }

    public IEnumerator OpenDoor(GameObject door, float distance, float speed, int direction = 1, bool isHorizontal = false)   // 
    {
        if (doorSFX != null) SoundManager.Instance.PlaySFX(doorSFX, doorVolume);

        Vector3 targetPos = door.transform.position;
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

        while ((targetPos - door.transform.position).magnitude > 0)
        {
            door.transform.position = Vector2.MoveTowards(door.transform.position, targetPos, speed * Time.deltaTime);
            yield return null;
        }
    }
}
