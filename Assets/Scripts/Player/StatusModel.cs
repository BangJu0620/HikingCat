using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusModel : MonoBehaviour
{
    [SerializeField]
    private float baseSpeed = 3.0f;

    public float Speed => baseSpeed;

    [SerializeField]
    private float baseJump = 2.0f;

    public float JumpForce => baseJump;
}
