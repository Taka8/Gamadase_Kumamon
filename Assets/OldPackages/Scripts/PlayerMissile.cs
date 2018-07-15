using UnityEngine;

namespace OldPackage
{
    [RequireComponent(typeof(CircleCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMissile : MonoBehaviour
    {

        [SerializeField] Vector2 initVelocity = new Vector2(10, 8);
        [SerializeField] AudioClip bounseClip;

        void Start()
        {

            GetComponent<Rigidbody2D>().velocity = Vector2.up * initVelocity.y + (Vector2)transform.forward * initVelocity.x;
            Destroy(gameObject, 3.0f);

        }

        public void OnCollisionEnter2D(Collision2D c)
        {

            SoundManager.Instance.PlaySe(bounseClip);

        }

    }
}