using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lever : MonoBehaviour
{
    [SerializeField] GameObject rightUpDoorObj;
    [SerializeField] GameObject leftDownDoorObj;
    [SerializeField] bool isHorizontal;

    Door rightUpDoor;
    Door leftDownDoor;

    bool isOpened = false;
    bool canInteract = false;

    private void Awake()
    {
        rightUpDoor = rightUpDoorObj.GetComponent<Door>();
        leftDownDoor = leftDownDoorObj.GetComponent<Door>();
    }

    private void Update()
    {
        if (!isOpened && canInteract)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                InteractLever();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player"))
        {
            canInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player"))
        {
            canInteract = false;
        }
    }

    void InteractLever()
    {
        // 사운드 재생

        StartCoroutine(rightUpDoor.OpenDoor(1, isHorizontal));
        StartCoroutine(leftDownDoor.OpenDoor(-1, isHorizontal));
        isOpened = true;
    }
}
