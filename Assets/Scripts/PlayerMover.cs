using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(GroundChecker))]
    public class PlayerMover : MonoBehaviour
    {

        [SerializeField] float moveSpeed = 5f;
        [SerializeField] float AirialMoveSpeed = 200f;
        [SerializeField] float initialJumpPower = 8f;
        [SerializeField] float additionalJumpPower = 400f;
        [SerializeField] Rigidbody2D rb;

        [SerializeField] Animator animator;

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
            if (groundChecker.IsGrounded)
            {
                Vector2 vel = rb.velocity;

                vel.x = moveSpeed * h;

                rb.velocity = vel;

                Turn(h);
            }
            else
            {
                rb.AddForce(Vector2.right * AirialMoveSpeed * h * Time.fixedDeltaTime);
                ClampVelocityX(moveSpeed);
            }

            animator.SetFloat("h", h);
            animator.SetFloat("y", rb.velocity.y);
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

        void Turn(float h)
        {
            Vector3 afterScale = Vector3.one;

            if (h == 0)
            {
                return;
            }

            afterScale.x = h < 0 ? -1 : 1;
            transform.localScale = afterScale;
        }

        void ClampVelocityX(float x)
        {
            Vector2 afterVelocity = rb.velocity;

            afterVelocity.x = Mathf.Clamp(afterVelocity.x, -x, x);

            rb.velocity = afterVelocity;
        }

    }
}