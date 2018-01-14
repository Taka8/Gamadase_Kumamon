using UnityEngine;

public class CoinTrigger : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(transform.parent.gameObject);
    }

}
