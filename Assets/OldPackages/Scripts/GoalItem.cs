using System.Collections;
using UnityEngine;

namespace OldPackage
{
    public class GoalItem : MonoBehaviour
    {

        void OnTriggerEnter2D(Collider2D collision)
        {
            Destroy(gameObject);
        }

    }
}