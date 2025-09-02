using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusModel : MonoBehaviour
{
    [SerializeField]
    private float baseSpeed = 3.0f;

    public float Speed => baseSpeed;

    [SerializeField]
    private float baseJump = 6.3f;
    [SerializeField]
    private float middleJump = 8.9f;
    [SerializeField]
    private float maxJumpForce = 10.8f;

    public float GetJumpForce(float chargeRange)
    {
        if(chargeRange < 0.33f)
        {
            return baseJump;
        }

        if(chargeRange < 0.66f)
        {
            return middleJump;
        }

        return maxJumpForce;
    }

}
