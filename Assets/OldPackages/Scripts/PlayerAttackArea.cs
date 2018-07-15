using UnityEngine;

namespace OldPackage
{
    public class PlayerAttackArea : MonoBehaviour
    {

        [SerializeField] bool destroyOnEnter;
        [SerializeField] GameObject destroyObject;

        private void OnTriggerEnter2D(Collider2D collision)
        {

            if (destroyOnEnter) Destroy(destroyObject);

        }

    }
}