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

    private bool isGround = false;

    [SerializeField] protected Vector2 velocity;
    private Vector2 groundNormal = Vector2.up;

    [SerializeField] private float minGroundY = 0.995f;
    [SerializeField] private float minMagnitudeSlideDir = 0.0001f;
    protected virtual void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        if(body == null)
        {
            body = gameObject.AddComponent<Rigidbody2D>();
        }

        body.isKinematic = true; 
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.useLayerMask = true;
        contactFilter.useTriggers = false;
    }

    protected virtual void FixedUpdate()
    {
        if (body != null)
        {
            if (!isGround)
            {
                velocity += Physics2D.gravity * gravityModifier * Time.fixedDeltaTime;
            }

            PerformMovement(velocity * Time.fixedDeltaTime);
            
        }
    }
    private bool isSlide = false;
    protected virtual void PerformMovement(Vector2 dir)
    {
        var distance = dir.magnitude; if (distance > minMoveDistance)
        {
            var cnt = body.Cast(dir, contactFilter, hitBuffer, distance + shellRadius); if (cnt == 0) { isGround = false; }
            if (cnt == 0) isSlide = false;
            for (int i = 0; i < cnt; i++)
            { 
                // Trigger 타입 콜라이더 무시
                if (hitBuffer[i].collider.isTrigger) continue; 
                var currentNormal = hitBuffer[i].normal; 
                // 사선에 충돌 시
                if (currentNormal.y < minGroundY)
                {
                    if (!isSlide) { Debug.Log("New Slide"); }
                    isSlide = true;
                    isGround = false;

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
                // 평면 위에 충돌 시
                else 
                {
                    isSlide = false;
                    Debug.Log("평지");
                    if (!isGround)
                    {
                        isGround = true; 
                        velocity.x = 0; 
                        dir.x = 0; 
                    }
                    velocity.y = 0; 
                    dir.y = 0;
                    currentNormal = currentNormal.normalized;
                    groundNormal = Vector2.Lerp(groundNormal, currentNormal, 0.5f); 
                    break; 
                }

                var modifiedDistance = Mathf.Max(hitBuffer[i].distance - shellRadius, 0); 
            }
            var moveDistance = dir.normalized * distance; 
            body.MovePosition(body.position + moveDistance); 
        }
    }
    

    private void Update()
    {
        if (isGround) velocity = Vector2.zero;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (isGround) velocity.x = -5f;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (isGround) velocity.x = 5f;
        }
    }
}
