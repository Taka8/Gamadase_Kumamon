using System.Collections;
using UnityEngine;

public class GoalItem : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);    
    }

}
