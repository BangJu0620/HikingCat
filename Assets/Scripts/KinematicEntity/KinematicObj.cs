using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class KinematicObj : MonoBehaviour
{
    [SerializeField]
    protected float gravityModifier = 1f;

    [SerializeField] private Rigidbody2D body;
    [SerializeField] protected ContactFilter2D contactFilter;
    [SerializeField] protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];

    [SerializeField] protected const float minMoveDistance = 0.001f;
    [SerializeField] protected const float shellRadius = 0.05f;

    public bool isGround { get; private set; } = false;

    [SerializeField] protected Vector2 velocity;

    private Vector2 additionalVelocty;
    private Vector2 groundNormal = Vector2.up;

    [SerializeField] private float minGroundY = 0.995f;
    [SerializeField] private float minMagnitudeSlideDir = 0.0001f;
    private bool isJump = false;

    protected virtual void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        if(body == null)
        {
            body = gameObject.AddComponent<Rigidbody2D>();
        }

        body.isKinematic = true; 
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.layerMask = contactFilter.layerMask & ~LayerMask.GetMask("Structure");
        contactFilter.useLayerMask = true;
        contactFilter.useTriggers = false;
    }

    protected virtual void FixedUpdate()
    {
        if (body != null)
        {
            if (gravityModifier > 0f)
            {
                if (velocity.y < 0)
                {
                    velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
                }
                else
                {
                    velocity += Physics2D.gravity * Time.deltaTime;
                }
            }
            else
            {
                velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
            }

            // Add External Force
            velocity += additionalVelocty;

            // Calculate Movement
            var deltaPos = velocity * Time.deltaTime;
            var moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);
            var move = moveAlongGround * deltaPos.x;

            PerformMovement(move, false);

            move = Vector2.up * deltaPos.y;

            PerformMovement(move, true);


            // Reset External Force
            velocity -= additionalVelocty;

            // Decay External Force
            DecayAdditionalVelocity();

        }
    }
    private bool isSlide = false;


    public virtual void PerformMovement(Vector2 dir, bool yMovement)
    {
        var distance = dir.magnitude;
        if (distance > minMoveDistance)
        {
            // Check hit buffer
            var cnt = body.Cast(dir, contactFilter, hitBuffer, distance + shellRadius);
            if(cnt == 0)
            {
                isGround = false;
                isSlide = false;
            }
            for (int i = 0; i < cnt; i++)
            {
                if (hitBuffer[i].collider.isTrigger)
                {
                    continue;
                }

                var currentNormal = hitBuffer[i].normal;
                // Check Bottom hit
                if (currentNormal.y > minGroundY)
                {
                    isGround = true;
                    isJump = false;
                    if (yMovement)
                    {
                        velocity.y = 0f;
                        groundNormal = currentNormal;
                    }
                }
                else
                {
                    if (!isSlide) { Debug.Log("New Slide"); }
                    isSlide = true;
                    isGround = false;
                    isJump = false;

                    // 경사면 방향 계산
                    Vector2 g = Physics2D.gravity.normalized;
                    Vector2 slideDir = g - Vector2.Dot(g, currentNormal) * currentNormal;
                    if (slideDir.sqrMagnitude < minMagnitudeSlideDir) slideDir = g;
                    slideDir.Normalize();

                    // 기존 속도 투영 + 중력 가속 추가
                    float speedAlongSlide = Vector2.Dot(velocity, slideDir);
                    Vector2 velocityAlongSlide = slideDir * speedAlongSlide;
                    Vector2 gravityAlongSlide = Vector2.Dot(Physics2D.gravity * gravityModifier, slideDir) * slideDir;

                    velocity = velocityAlongSlide + gravityAlongSlide * Time.fixedDeltaTime;

                    dir = velocity * Time.fixedDeltaTime;

                    if (dir.y > 0)
                    {
                        Debug.Log($"{slideDir} is SlideDir");
                    }
                }
                // Check Head hit
                if (currentNormal.y < -0.5f)
                {
                    velocity.y = Mathf.Min(velocity.y, 0);
                }


                var modifiedDistance = hitBuffer[i].distance - shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
            var moveDistance = dir.normalized * distance;
            body.position += moveDistance;
        }
    }

    public void AddForce(Vector2 force)
    {
        additionalVelocty += force;
    }

    private void DecayAdditionalVelocity()
    {
        additionalVelocty.x *= (1 - 0.1f);
        if (additionalVelocty.y > 0)
        {
            additionalVelocty += gravityModifier * Physics2D.gravity * Time.fixedDeltaTime;
        }
        else
        {
            additionalVelocty.y = 0f;
        }
        if (additionalVelocty.magnitude <= 0.01f)
        {
            additionalVelocty = Vector2.zero;
        }
    }

    public void InputHandler(Vector2 inputVec)
    {
        if (isGround || isJump)
        {
            velocity.x = inputVec.x;
        }
    }

    public void Jump(float jumpForce, bool isForce = false)
    {
        if (isGround || isForce)
        {
            isJump = true;
            velocity.y = jumpForce;
        }
    }
}
