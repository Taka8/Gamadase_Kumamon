using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{

    public float duration;

    [SerializeField] Animator animator;
    [SerializeField] GroundChecker gc;
    [SerializeField] Rigidbody2D rb;

    void Update()
    {
        // 上昇
        if (rb.velocity.y > 0)
        {
            animator.CrossFade("Up", duration);
        }
        else if (rb.velocity.y < 0)
        {
            animator.CrossFade("Down", duration);
            if (gc.IsGrounded)
            {
                animator.CrossFade("New State", duration);
            }
        }
    }
    
}
