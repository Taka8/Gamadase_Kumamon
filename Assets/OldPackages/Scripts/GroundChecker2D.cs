using UnityEngine;

namespace OldPackage
{
    public class GroundChecker2D : MonoBehaviour
    {

        [SerializeField] Vector2 offset;
        [SerializeField] float radius;
        [SerializeField] LayerMask groundLayer;

        bool isGrounded;

        public bool IsGrounded
        {

            get
            {

                return isGrounded;

            }

        }

        void FixedUpdate()
        {

            if (Physics2D.OverlapCircle(transform.position + transform.forward * offset.x + transform.up * offset.y, radius, groundLayer))
            {

                isGrounded = true;

            }
            else
            {

                isGrounded = false;

            }

        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position + transform.forward * offset.x + transform.up * offset.y, radius);
        }

    }
}