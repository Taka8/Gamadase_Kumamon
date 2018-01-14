using UnityEngine;
using System.Collections;
using System;

public class KintaScript : EnemyBase
{

    [SerializeField] float bulletSpeed;
    [SerializeField] BulletScript bullet;

    [SerializeField] Transform offset;
    [SerializeField] Rigidbody hipRigidbody;

    private IEnumerator Attack()
    {

        yield return new WaitForSeconds(2f);

        while (true)
        {

            foreach (Player2D p in GameController.Instance.Players)
            {

                if (p == null) continue;

                Vector3 dir = ((p.transform.position + Vector3.up) - (transform.position + Vector3.up * 2)).normalized;

                Fire(dir);

            }

            yield return new WaitForSeconds(2.5f);

        }

    }

    void Start()
    {

        hipRigidbody.isKinematic = true;

        StartCoroutine(Attack());

    }

    void OnEnable()
    {

        StartCoroutine(Attack());

    }

    void OnDisable()
    {

        StopAllCoroutines();

    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.GetComponent<PlayerAttackArea>())
        {

            Dead(Mathf.Sign(transform.position.x - collision.transform.position.x));

        }

    }

    private void Fire(Vector3 dir)
    {

        Instantiate(bullet, transform.position + Vector3.up * 2, Quaternion.identity).SetBulletScript(dir, bulletSpeed, "EnemyAttack");

    }

    public override void Damage()
    {

        Dead();

    }


    protected override void Dead()
    {

        Destroy(gameObject);

    }

    private void Dead(float dir)
    {

        offset.parent = null;

        hipRigidbody.isKinematic = false;
        deadParticle.Play();

        hipRigidbody.AddForce(new Vector3(dir, 2, 0) * 400f);

        Instantiate(GameController.Instance.CoinPrefab, transform.position + Vector3.up * 1.5f, Quaternion.identity);

        SoundManager.Instance.PlaySe(deadSE);

        Destroy(hipRigidbody.gameObject, 2f);
        Destroy(gameObject);

    }

}
