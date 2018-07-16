using UnityEngine;

public class GroundChecker : MonoBehaviour
{

    [SerializeField] float offsetY;
    [SerializeField] float radius;
    [SerializeField] LayerMask groundLayer;

    [SerializeField] bool isGrounded;

    public bool IsGrounded { get { return isGrounded; } }

    void FixedUpdate()
    {
        isGrounded = CheckGrounded();
    }

    bool CheckGrounded()
    {
        return Physics2D.OverlapCircle((Vector2)transform.position + Vector2.up * offsetY, radius, groundLayer);
    }

#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + Vector3.up * offsetY, radius);
    }
#endif

}
