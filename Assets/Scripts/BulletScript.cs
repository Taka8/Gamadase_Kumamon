using UnityEngine;

public class BulletScript : MonoBehaviour
{

    private Vector3 moveDirection;
    private float speed;

    void Update()
    {

        // 移動
        transform.Translate(moveDirection * speed * Time.deltaTime);

    }

    public void SetBulletScript(Vector3 dir, float speed, string tag)
    {

        moveDirection = dir;

        this.speed = speed;

        this.tag = tag;

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

}
