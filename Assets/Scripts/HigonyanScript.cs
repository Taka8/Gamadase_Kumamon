using UnityEngine;

public class HigonyanScript : EnemyBase
{

    [Header("Higonyan")]
    [SerializeField]
    Rigidbody hipRigidbody;
    [SerializeField] Transform offset;
    [SerializeField] GroundChecker2D forwardChecker;

    Rigidbody[] rigidbodies;

    protected override void Reset()
    {
        base.Reset();
    }

    void Awake()
    {

        rigidbodies = GetComponentsInChildren<Rigidbody>();
    }


    void Start()
    {

        SetIsKinematicAll(true, rigidbodies);
        MoveDirection = -1;

    }

    void FixedUpdate()
    {

        // 方向転換
        if (forwardChecker.IsGrounded == true)
        {
            MoveDirection *= -1;
            offset.rotation = Quaternion.LookRotation(Vector3.right * MoveDirection);
        }

        // 移動
        Move();

    }

    protected override void Move()
    {
        transform.position += Vector3.right * MoveDirection * moveSpeed * Time.fixedDeltaTime;
    }

    protected override void Dead()
    {

        SetIsKinematicAll(false, rigidbodies);
        animator.enabled = false;
        deadParticle.Play();

        offset.parent = null;

        hipRigidbody.velocity = Vector3.up * 50f;

        Instantiate(GameController.Instance.CoinPrefab, transform.position + Vector3.up * 1.5f, Quaternion.identity);

        SoundManager.Instance.PlaySe(deadSE);

        Destroy(gameObject);
        Destroy(offset.gameObject, 2f);

    }

    public override void Damage()
    {

        Dead();

    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.GetComponent<PlayerAttackArea>())
        {
            Damage();
        }

    }

}
