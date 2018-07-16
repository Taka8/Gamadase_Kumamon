using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(GroundChecker))]
public class PlayerMover : MonoBehaviour
{

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float initialJumpPower = 8f;
    [SerializeField] float additionalJumpPower = 400f;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] GroundChecker groundChecker;

    void Reset()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 2;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        groundChecker = GetComponent<GroundChecker>();
    }

    public void FixedMove(float h)
    {
        Vector2 vel = rb.velocity;

        vel.x = moveSpeed * h;

        rb.velocity = vel;
    }

    public void FixedJump(bool jump)
    {
        if (jump)
        {
            if (groundChecker.IsGrounded)
            {
                Vector2 vel = rb.velocity;

                vel.y = initialJumpPower;

                rb.velocity = vel;
            }
            else
            {
                if (rb.velocity.y > 0)
                {
                    rb.AddForce(Vector2.up * additionalJumpPower * Time.fixedDeltaTime);
                }
            }
        }
    }

}
