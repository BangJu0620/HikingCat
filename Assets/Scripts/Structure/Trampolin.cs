using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampolin : MonoBehaviour
{
    // 플레이어 필요한가
    // Player player;

    [SerializeField] float jumpPower;

    bool isUpPos = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        // 플레이어인지 검사
        //KinematicObj kineObj = collision.gameObject.TryGetComponent<KinematicObj>(out kineObj)
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
                kineObj.AddForce(Vector2.up * jumpPower);
            }
            isUpPos = false;
        }

        //if (collision.gameObject.CompareTag("Player"))
        //{
        //    foreach (var contact in collision.contacts)
        //    {
        //        // 어디에서 충돌했는지 검사하기, 옆면이면 무시하기
        //        if (Vector3.Dot(contact.normal, Vector3.up) < -0.995f)
        //        {
        //            isUpPos = true;
        //            break;
        //        }
        //    }
        //    if (isUpPos)
        //    {
        //        // 플레이어한테 점프 시키기
        //        collision.rigidbody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        //    }
        //}
    }
}
