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
        // �÷��̾����� �˻�
        if (collision.gameObject.TryGetComponent(out KinematicObj kineObj))
        {
            foreach (var contact in collision.contacts)
            {
                // ��𿡼� �浹�ߴ��� �˻��ϱ�, �����̸� �����ϱ�
                if (Vector2.Dot(contact.normal, Vector2.up) < -0.9f)
                {
                    isUpPos = true;
                    break;
                }
            }
            if (isUpPos)
            {
                anim.SetTrigger("isActivate");
                // �÷��̾����� ���� ��Ű��
                kineObj.Jump(jumpPower, true);
                if (trampolinSFX != null) SoundManager.Instance.PlaySFX(trampolinSFX, trampolinVolume);
                isUpPos = false;
            }
        }
    }

}
