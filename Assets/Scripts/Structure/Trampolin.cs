using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampolin : MonoBehaviour
{
    [SerializeField] float jumpPower;

    [SerializeField] AudioClip trampolinSFX;
    [SerializeField] float trampolinVolume;
    [SerializeField] Animator anim;

    bool isUpPos = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 플레이어인지 검사
        if (collision.gameObject.TryGetComponent(out KinematicObj kineObj))
        {
            foreach (var contact in collision.contacts)
            {
                // 어디에서 충돌했는지 검사하기, 옆면이면 무시하기
                if (Vector2.Dot(contact.normal, Vector2.up) < -0.9f)
                {
                    isUpPos = true;
                    break;
                }
            }
            if (isUpPos)
            {
                anim.SetTrigger("isActivate");
                // 플레이어한테 점프 시키기
                kineObj.Jump(jumpPower, true);
                if (trampolinSFX != null) SoundManager.Instance.PlaySFX(trampolinSFX, trampolinVolume);
                isUpPos = false;
            }
        }
    }

}
