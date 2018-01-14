using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DamageArea : MonoBehaviour
{

    [SerializeField] bool DestroyOnEnter;
    [SerializeField] GameObject destroyObject;

    void OnTriggerEnter2D(Collider2D other)
    {

        if (DestroyOnEnter == true)
        {

            Destroy(destroyObject);

        }

    }

}