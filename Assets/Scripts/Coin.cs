using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Coin : MonoBehaviour
{

    [SerializeField] float duration = 2f;
    [SerializeField] Vector2 initialVelocity = Vector2.up * 10f;

    void Start()
    {

        Destroy(gameObject, duration);
        GetComponent<Rigidbody2D>().velocity = initialVelocity;

    }

}
