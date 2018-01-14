using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public abstract class EnemyBase : Character2D
{

    [Header("CharacterBaseScript")]
    [SerializeField]
    protected float moveSpeed = 2.0f;
    [SerializeField] protected AudioClip deadSE;

    [Header("Components")]
    [SerializeField]
    protected Animator animator;
    [SerializeField] protected ParticleSystem deadParticle;

    float moveDirection;

    protected float MoveDirection
    {
        get
        {
            moveDirection = Mathf.Clamp(moveDirection, -1, 1);
            return moveDirection;
        }

        set
        {
            value = Mathf.Clamp(value, -1, 1);
            moveDirection = value;
        }
    }

    protected virtual void Reset()
    {
        animator = GetComponent<Animator>();
    }

    protected virtual void Move()
    {

    }

    protected abstract void Dead();

    public abstract void Damage();

}
