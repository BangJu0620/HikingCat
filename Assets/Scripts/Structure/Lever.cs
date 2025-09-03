using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lever : MonoBehaviour
{
    [Header("문")]
    //[SerializeField] GameObject rightUpDoorObj;
    //[SerializeField] GameObject leftDownDoorObj;
    [SerializeField] Door door;
    //[SerializeField] bool isHorizontal;
    //[SerializeField] float distance;
    //[SerializeField] float speed;

    //Door rightUpDoor;
    //Door leftDownDoor;

    [SerializeField] bool isOpened = false;

    [Header("레버")]
    [SerializeField] float colliderSize;
    [SerializeField] Animator anim;
    [SerializeField] AudioClip leverSFX;
    [SerializeField] float leverVolume;

    Collider2D collider;

    [SerializeField] bool canInteract = false;

    private void Awake()
    {
        //rightUpDoor = rightUpDoorObj.GetComponent<Door>();
        //leftDownDoor = leftDownDoorObj.GetComponent<Door>();
        collider = GetComponent<Collider2D>();
        SetSizeTrigger();
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
        if (leverSFX != null) SoundManager.Instance.PlaySFX(leverSFX, leverVolume);

        //StartCoroutine(rightUpDoor.OpenDoor(distance, speed, 1, isHorizontal));
        //StartCoroutine(leftDownDoor.OpenDoor(distance, speed, -1, isHorizontal));

        door.Open();

        isOpened = true;
        anim.SetBool("IsOpened", true);
    }

    void SetSizeTrigger()
    {
        if(collider is BoxCollider2D)
        {
            BoxCollider2D box = collider as BoxCollider2D;
            box.size = Vector2.one * colliderSize;
        }
        else if(collider is CircleCollider2D)
        {
            CircleCollider2D circle = collider as CircleCollider2D;
            circle.radius = colliderSize;
        }
        else
        {
            return;
        }
    }
}
