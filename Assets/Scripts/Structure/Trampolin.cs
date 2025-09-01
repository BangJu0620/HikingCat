using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampolin : MonoBehaviour
{
    [SerializeField] float jumpPower;

    bool isUpPos = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �÷��̾����� �˻�
        if (collision.gameObject.TryGetComponent(out KinematicObj kineObj))
        {
            foreach (var contact in collision.contacts)
            {
                Debug.Log(Vector2.Dot(contact.normal, Vector2.up));
                // ��𿡼� �浹�ߴ��� �˻��ϱ�, �����̸� �����ϱ�
                if (Vector2.Dot(contact.normal, Vector2.up) < -0.995f)
                {
                    isUpPos = true;
                    break;
                }
            }
            if (isUpPos)
            {
                // �÷��̾����� ���� ��Ű��
                kineObj.Jump(jumpPower, true);
            }
            isUpPos = false;
        }
    }

}
