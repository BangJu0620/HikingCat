using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lever : MonoBehaviour
{
    [SerializeField] GameObject doorObj;

    Door door;

    bool isOpened = false;
    bool canInteract = false;

    private void Awake()
    {
        door = doorObj.GetComponent<Door>();
    }

    private void Update()
    {
        if (!isOpened && canInteract)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(door.OpenDoor());
                isOpened = true;
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
}
