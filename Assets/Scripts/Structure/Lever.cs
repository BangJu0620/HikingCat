using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lever : MonoBehaviour, IInteractable
{
    [Header("문")]
    [SerializeField] Door door;

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
        collider = GetComponent<Collider2D>();
        SetSizeCollider();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);

        if(collision.gameObject.TryGetComponent(out Interaction interaction))
        {
            Debug.Log("들어옴");
            canInteract = true;
            interaction.curInteractable = this;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);

        if (collision.gameObject.TryGetComponent(out Interaction interaction))
        {
            Debug.Log("나감");
            canInteract = true;
            interaction.curInteractable = null;
        }
    }

    void SetSizeCollider()
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

    public void Interact()
    {
        if (isOpened || !canInteract) return;

        // 사운드 재생
        if (leverSFX != null) SoundManager.Instance.PlaySFX(leverSFX, leverVolume);

        door.Open();

        isOpened = true;
        anim.SetBool("IsOpened", true);
    }
}
