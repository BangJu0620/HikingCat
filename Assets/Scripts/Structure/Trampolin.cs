using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampolin : MonoBehaviour
{
    [SerializeField] float jumpPower;

    bool isUpPos = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 플레이어인지 검사
        if (collision.gameObject.TryGetComponent(out KinematicObj kineObj))
        {
            foreach (var contact in collision.contacts)
            {
                Debug.Log(Vector2.Dot(contact.normal, Vector2.up));
                // 어디에서 충돌했는지 검사하기, 옆면이면 무시하기
                if (Vector2.Dot(contact.normal, Vector2.up) < -0.995f)
                {
                    isUpPos = true;
                    break;
                }
            }
            if (isUpPos)
            {
                // 플레이어한테 점프 시키기
                kineObj.Jump(jumpPower, true);
            }
            isUpPos = false;
        }
    }

}
